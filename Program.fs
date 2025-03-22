module FpAssignments.Program

open System.Collections.Generic
open Window
open GUI
open MazeGenerator

// let rect = 
//     let content = List<string>()
//     content.Add "0123456789"
//     content.Add "    01    "
//     createDrawRect TopCenter TopCenter (0, 1) (10, 2) content

[<EntryPoint>]
let main args = 
    let window = create 120 45 30
    addContent window (generateMaze window 40 15 100000)
    mainLoop window
    0