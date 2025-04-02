open RMGPA.Core
open RMGPA.Core.Evaluator

[<EntryPoint>]
let main _ =
    let myCell = 
        { Id = "cell-001"
          Role = Processor
          Memory = Map.empty
          Organelles = [] }

    interpretGoal myCell (Maintain ("energy", 0.8))
    0
