module RMGPA.Core.Field

type Field =
    { Name: string
      Gradient: Map<(int * int), float> }

let readAt (x, y) field =
    field.Gradient |> Map.tryFind (x, y) |> Option.defaultValue 0.0
