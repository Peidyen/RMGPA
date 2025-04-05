namespace RMGPA.CLI

module Main =

    open System
    open RMGPA.Core
    open RMGPA.Core.Tissue

    // Clear the terminal for animation effect
    let clearScreen () =
        Console.Clear()

    // Generate a 10x10 grid of cells
    let generateGridCells () =
        [ for x in 0..9 do
            for y in 0..9 ->
                let loc = Grid(x, y)
                let role =
                    if x = y then Role.Control
                    elif (x + y) % 4 = 0 then Role.Memory
                    elif (x + y) % 5 = 0 then Role.Sensor
                    else Role.Processor

                { Id = Guid.NewGuid().ToString()
                  Role = role
                  Location = loc
                  Memory = Map.empty
                  Organelles = [ processor; memory; signal ] } ]

    // Convert memory (cpu_cycles) into a shaded ASCII character
    let memoryToChar cell =
        let cycles =
            match cell.Memory |> Map.tryFind "cpu_cycles" with
            | Some(:? int as i) -> i
            | _ -> 0

        match cycles with
        | 0 -> '·'
        | 1 | 2 -> '░'
        | 3 | 4 | 5 -> '▒'
        | 6 | 7 | 8 | 9 -> '▓'
        | _ -> '█'

    // Render the tissue as ASCII heatmap of CPU activity
    let renderGrid cells =
        for y in 0..9 do
            for x in 0..9 do
                let symbol =
                    cells
                    |> List.tryFind (fun c -> c.Location = Grid(x, y))
                    |> Option.map memoryToChar
                    |> Option.defaultValue '.'
                printf "%c " symbol
            printfn ""

    // Simulate evolution over ticks
    let runSimulation cells ticks =
        let rec loop currentCells tick =
            if tick > ticks then currentCells
            else
                clearScreen ()
                printfn "--- Tick %d ---\n" tick
                let updated =
                    currentCells
                    |> List.map (fun cell ->
                        cell.Organelles
                        |> List.fold (fun c o -> o.Execute c) cell)

                renderGrid updated
                System.Threading.Thread.Sleep(150)
                loop updated (tick + 1)
        loop cells 1

    [<EntryPoint>]
    let main _ =
        let cells = generateGridCells ()
        let _ = runSimulation cells 100
        0
