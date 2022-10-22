using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UniRx;
using UnityEngine;

namespace Initialize.Core
{
    public class Initializer
    {
        public event Action OnAllCompleted;

        private Dictionary<Type, IInitTask> _tasks = new Dictionary<Type, IInitTask>();
        private Dictionary<Type, IInitTask> _finalizeTasks = new Dictionary<Type, IInitTask>();
        private Dictionary<Type, List<Type>> _requiredTasks = new Dictionary<Type, List<Type>>();
        private HashSet<Type> _completedTasks = new HashSet<Type>();
        private List<Type> _todoList;
        
        public void RegisterTask(IInitTask task)
        {
            var taskType = task.GetType();
            _tasks.Add(taskType, task);
            _requiredTasks.Add(taskType, new List<Type>());
            if (taskType.HasAttribute<RequireTaskAttribute>())
            {
                var requiredTasks = taskType.GetCustomAttributes<RequireTaskAttribute>().Select(e => e.Require)
                    .ToArray();
                if (requiredTasks.Length > 0)
                {
                    _requiredTasks[taskType].AddRange(requiredTasks);
                }
            }
        }

        public void Run()
        {
            _todoList = _tasks.Keys.ToList();
            StartTasks();
        }

        private void StartTasks()
        {
            var toStart = GetTasksToStart();
            _todoList.RemoveAll(toStart.Contains);
            foreach (var taskType in toStart)
            {
                TryStartTask(taskType);
            }
        }
        
        private Type[] GetTasksToStart()
        {
            return _todoList.Where(e => _requiredTasks[e].All(r => _completedTasks.Contains(r))).ToArray();
        }
        
        private void TryStartTask(Type taskType)
        {
            Scheduler.MainThread.Schedule(() =>
            {
                var task = _tasks[taskType];
                var requirements = _requiredTasks[taskType];
                if (requirements.All(e => _completedTasks.Contains(e)))
                {
                    _todoList.Remove(taskType);
                    task.Completed += OnTaskComplete;
                    Debug.Log($"[INIT] start Task {task.GetType().Name}");
                    task.Run();
                }
                else
                {
                    _todoList.Add(taskType);
                }
            });
        }

        private void OnTaskComplete(IInitTask task)
        {
            task.Completed -= OnTaskComplete;
            Debug.Log($"[INIT] complete Task {task.GetType().Name}");
            _completedTasks.Add(task.GetType());
            if (_completedTasks.Count == _tasks.Count)
            {
                OnAllCompleted?.Invoke();
            }
            else
            {
                StartTasks();
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireTaskAttribute : Attribute
    {
        public Type Require;

        public RequireTaskAttribute(Type type)
        {
            Require = type;
        }
    }
}