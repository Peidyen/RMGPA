namespace RMGPA.Core

open System

module Tissue =

    open RMGPA.Core

    let processor =
        { Kind = OrganelleKind.Processor
          Name = "ALU-1"
          Execute = fun cell ->
              let updatedMem =
                  cell.Memory
                  |> Map.change "cpu_cycles" (fun c ->
                      let current =
                          match c with
                          | Some o ->
                              match o :?> obj with
                              | :? int as i -> i
                              | _ -> 0
                          | None -> 0
                      Some(box (current + 1)))
              { cell with Memory = updatedMem } }

    let memory =
        { Kind = OrganelleKind.Memory
          Name = "MemCore-1"
          Execute = fun cell ->
              let updatedMem =
                  cell.Memory
                  |> Map.add "recent_input" (box 42)
              { cell with Memory = updatedMem } }

    let signal =
        { Kind = OrganelleKind.Signal
          Name = "NetLink-1"
          Execute = fun cell ->
              printfn "Cell %s emits signal 'ping'" cell.Id
              cell }

    let buildCellFromOrganelles id role location organelles =
        { Id = id
          Role = role
          Location = location
          Memory = Map.empty
          Organelles = organelles }

    type Tissue =
        { Name: string
          Cells: Cell list }

    let buildTissue name =
        let locations = [ for x in 0..4 do for y in 0..4 -> Grid(x, y) ]
        let cells =
            locations
            |> List.map (fun loc ->
                let role =
                    match loc with
                    | Grid(2, 2) -> Role.Control
                    | Grid(x, y) when (x + y) % 4 = 0 -> Role.Memory
                    | Grid(x, y) when (x + y) % 5 = 0 -> Role.Sensor
                    | _ -> Role.Processor

                let organelles = [ processor; memory; signal ]
                buildCellFromOrganelles (Guid.NewGuid().ToString()) role loc organelles)
        { Name = name; Cells = cells }

    module Exports =
        let processor = processor
        let memory = memory
        let signal = signal
