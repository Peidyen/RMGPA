namespace RMGPA.Core

type Role =
    | Processor
    | Memory
    | Sensor
    | Control

type Organelle =
    { Kind: string
      State: obj }

type Cell =
    { Id: string
      Role: Role
      Memory: Map<string, obj>
      Organelles: Organelle list }

type Goal =
    | Maintain of string * float
    | Achieve of string * obj
