using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using Microsoft.FSharp.Collections;                    // For FSharpList<>
using RMGPA.Core;                                      // For SimulationEngine
using static RMGPA.Core.SimulationEngine;              // To call generateGridCells, step, renderGrid

namespace RMGPA.UI;

public partial class MainWindow : Window
{
    private FSharpList<RMGPA.Core.Tissue.Cell> _cells; // ✅ Use the correct F# Cell type
    private int _tick;
    private DispatcherTimer _timer;

    public MainWindow()
    {
        InitializeComponent();

        _cells = generateGridCells(); // F# function: FSharpList<Cell>
        _tick = 0;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(150)
        };
        _timer.Tick += (_, _) => RunStep();

        StartButton.Click += (_, _) => _timer.Start();
    }

    private void RunStep()
    {
        _tick++;
        _cells = step(_cells); // step() returns FSharpList<Cell>
        OutputText.Text = $"--- Tick {_tick} ---\n\n" + renderGrid(_cells);

        if (_tick >= 100)
        {
            _timer.Stop();
        }
    }
}
