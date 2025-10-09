using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Code.Game
{
    public class ZombieSpawner : MonoBehaviour
    {
        [Header("Configurations")]
        [SerializeField] private ZombieSpawnConfig _config;
        [SerializeField] private ZombieStats _baseZombieStats;
        
        // --- Состояние спавнера ---
        private CancellationTokenSource _cancellationTokenSource;
        private float _currentSpawnRate;
        
        // --- Учет зомби (Senior-level) ---
        private readonly List<GameObject> _activeZombies = new List<GameObject>();


        private void Start()
        {
            // Устанавливаем начальное значение
            _currentSpawnRate = _config.initialSpawnRate;
            
            // Начинаем спавн
            StartSpawning();
        }

        private void OnDestroy()
        {
            // При уничтожении объекта отменяем все запущенные UniTask'и
            StopSpawning();
        }

        // --- УПРАВЛЕНИЕ СОСТОЯНИЕМ (CONTROL FLOW) ---
        public void StartSpawning()
        {
            // 1. Останавливаем предыдущую задачу, если она была запущена
            StopSpawning();
            
            // 2. Создаем новый токен для текущей задачи
            _cancellationTokenSource = new CancellationTokenSource();
            
            // 3. Запускаем асинхронную задачу
            SpawnLoopAsync(_cancellationTokenSource.Token).Forget();
            
            Debug.Log("Spawner: Started the main spawning loop.");
        }

        public void StopSpawning()
        {
            if (_cancellationTokenSource != null)
            {
                // Отправляем сигнал отмены всем задачам, использующим этот токен
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
                
                Debug.Log("Spawner: Spawning loop was cancelled.");
            }
        }
        
        // --- ОСНОВНАЯ АСИНХРОННАЯ ЛОГИКА ---
        private async UniTask SpawnLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                // 1. Ожидание с учетом токена отмены (НЕБЛОКИРУЮЩАЯ ОПЕРАЦИЯ)
                await UniTask.Delay(TimeSpan.FromSeconds(_currentSpawnRate), 
                                    ignoreTimeScale: false, 
                                    cancellationToken: token);

                // Если код дошел сюда, значит, токен НЕ был отменен
                if (token.IsCancellationRequested) return;

                TrySpawnZombie();

                // 2. Усложнение (изменение _spawnRate)
                IncreaseDifficulty();
            }
        }
        
        // --- ЛОГИКА СПАВНА И УПРАВЛЕНИЯ ЗОМБИ ---
        private void TrySpawnZombie()
        {
            // Очищаем список от мертвых/уничтоженных зомби (null)
            _activeZombies.RemoveAll(z => z == null);

            if (_activeZombies.Count >= _config.maxActiveZombies)
            {
                Debug.Log("Max zombie limit reached. Skipping spawn.");
                return;
            }
            
            // Вычисление позиции
            float x = Random.Range(_config.spawnZoneBounds.x, _config.spawnZoneBounds.y);
            float z = Random.Range(_config.spawnZoneBounds.x, _config.spawnZoneBounds.y);
            Vector3 spawnPosition = new Vector3(x, transform.position.y, z);

            // Спавн и настройка
            GameObject newZombie = Instantiate(_baseZombieStats.prefab, spawnPosition, Quaternion.identity);
            _activeZombies.Add(newZombie);
            
            // Здесь можно применить _baseZombieStats к новому зомби
            // (например, newZombie.GetComponent<Zombie>().Initialize(_baseZombieStats));
            
            Debug.Log($"Spawned a zombie. Active: {_activeZombies.Count}");
        }

        private void IncreaseDifficulty()
        {
            // Плавно уменьшаем время между спавнами, но не ниже минимального значения
            _currentSpawnRate = Mathf.Max(_config.minSpawnRate, 
                                          _currentSpawnRate - _config.rateDecreaseStep);

            Debug.Log($"Difficulty increased. New spawn rate: {_currentSpawnRate:F2}s");
        }
        
        // Для визуализации зоны спавна
        private void OnDrawGizmosSelected()
        {
            if (_config != null)
            {
                Gizmos.color = Color.yellow;
                float size = _config.spawnZoneBounds.y - _config.spawnZoneBounds.x;
                // Предполагаем, что зона - это квадрат в плоскости XZ
                Gizmos.DrawWireCube(transform.position, new Vector3(size, 1f, size));
            }
        }
    }

    internal class ZombieSpawnConfig 
    {
    }
}