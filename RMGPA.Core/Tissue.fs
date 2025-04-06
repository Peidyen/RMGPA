namespace RMGPA.Core

open RMGPA.Core
open RMGPA.Core.Genome

type Tissue = {
    Name: string
    Cells: Cell list
}

module Tissue =

    let create name (genomes: (string * Location * Genome) list) : Tissue =
        let cells =
            genomes
            |> List.map (fun (id, location, genome) -> decodeGenome id location genome)
        { Name = name; Cells = cells }

    let layout (tissue: Tissue) (layoutType: Layout) =
        match layoutType with
        | Grid (w, h) ->
            tissue.Cells
            |> List.iteri (fun i cell ->
                let x = i % w
                let y = i / w
                printfn $"Placing cell {cell.Id} at ({x},{y})"
            )
        | Chain count ->
            tissue.Cells
            |> List.take count
            |> List.iteri (fun i cell ->
                printfn $"Chain pos {i}: cell {cell.Id}"
            )
