open Game

[<EntryPoint>]
let main argv = 
    use g = new TargetGame()
    g.Run()
    0