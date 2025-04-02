namespace RMGPA.Core

type Role =
    | Processor
    | Memory
    | Sensor
    | Control

type Location =
    | Grid of int * int
    | Vector3 of float * float * float
    | Tag of string

type OrganelleKind =
    | Processor
    | Memory
    | Signal

type Cell =  // Forward declaration to allow use in Organelle
    { Id: string
      Role: Role
      Location: Location
      Memory: Map<string, obj>
      Organelles: Organelle list }

and Organelle =
    { Kind: OrganelleKind
      Name: string
      Execute: Cell -> Cell }
