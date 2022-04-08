﻿using System;
using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Windows;

namespace _Project.CodeBase.StaticData.UI
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}