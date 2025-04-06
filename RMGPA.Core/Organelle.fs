namespace RMGPA.Core

module OrganelleFactory =

    /// Default no-op behavior
    let defaultExecute : Cell -> Cell = id

    /// Constructs an organelle from kind and name
    let build (kind: OrganelleKind) (name: string) : Organelle =
        { Kind = kind
          Name = name
          Execute = defaultExecute }
