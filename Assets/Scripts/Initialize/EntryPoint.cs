﻿using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Zenject;

namespace Initialize
{
    public class EntryPoint : MonoBehaviour
    {
        private DiContainer _container;
        
        private void Start()
        {
            var context = ProjectContext.Instance;
            _container = context.Container;
        }
    }
}