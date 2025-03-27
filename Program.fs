module FpAssignments.Program

open FpAssignments.Window.Binding
open Window

[<EntryPoint>]
let main args = 
    let window = createWindow 120 45 30

    let exitBind = { key = System.ConsoleKey.Escape; func = fun () -> System.Environment.Exit 0 }
    addBinding window exitBind

    mainLoop window
    // addContent window (generateMaze window 40 15 100000)
    0