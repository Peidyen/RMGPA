namespace RMGPA.Core

open System
open RMGPA.Core
open RMGPA.Core.OrganelleFactory

module Genome =

    type Goal =
        | Maintain of string * float
        | Achieve of string * obj

    type Gene =
        | OrganelleGene of kind: OrganelleKind * name: string
        | GoalGene of Goal
        | RoleGene of Role

    type Genome = Gene list

    let private rand = Random()

    let private organelleKinds = [ OrganelleKind.Processor; OrganelleKind.Memory; OrganelleKind.Signal ]
    let private roles = [ Processor; Sensor; Control ]

    let private randomOrganelleKind () =
        organelleKinds |> List.item (rand.Next(organelleKinds.Length))

    let private randomRole () =
        roles |> List.item (rand.Next(roles.Length))

    let private randomName () =
        let names = [ "O1"; "O2"; "O3"; "O4" ]
        names.[rand.Next(names.Length)]

    let randomGene () =
        match rand.Next(3) with
        | 0 -> OrganelleGene (randomOrganelleKind (), randomName ())
        | 1 ->
            if rand.Next(2) = 0 then
                GoalGene (Maintain ("energy", rand.NextDouble() * 100.0))
            else
                GoalGene (Achieve ("output", box "signal"))
        | _ -> RoleGene (randomRole ())

    let randomGenome () : Genome =
        List.init (rand.Next(3, 6)) (fun _ -> randomGene ())

    let defaultExecute : Cell -> Cell = id

    let decodeGenome (id: string) (location: Location) (genome: Genome) : Cell =
        let mutable role = Processor
        let mutable organelles = []

        for gene in genome do
            match gene with
            | OrganelleGene (kind, name) ->
                organelles <- createOrganelle kind name defaultExecute :: organelles
            | RoleGene r -> role <- r
            | GoalGene _ -> () // Placeholder for future logic

        {
            Id = id
            Role = role
            Location = location
            Memory = Map.empty
            Organelles = organelles
        }
