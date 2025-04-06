namespace RMGPA.Core

type OrganelleKind =
    | Processor
    | Memory
    | Signal

type Role =
    | Processor
    | Sensor
    | Control

type Location = int * int

type Organelle = {
    Kind: OrganelleKind
    Name: string
    Execute: Cell -> Cell
}

and Cell = {
    Id: string
    Role: Role
    Location: Location
    Memory: Map<string, obj>
    Organelles: Organelle list
}



type Layout =
    | Grid of width: int * height: int
    | Chain of int
