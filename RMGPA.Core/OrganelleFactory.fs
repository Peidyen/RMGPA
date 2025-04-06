namespace RMGPA.Core

open RMGPA.Core

module OrganelleFactory =

    let createOrganelle (kind: OrganelleKind) (name: string) (execute: Cell -> Cell) : Organelle =
        {
            Kind = kind
            Name = name
            Execute = execute
        }
