module FpAssignments.Program

open System.Collections.Generic
open Window
open GUI
open MazeGenerator

let textBox = 
    let list = List<string> ()
    let mutable str = ""
    for _ = 0 to 99 do 
        str <- str + "a"
    list.Add str
    list.Add str
    createDrawRect TopLeft TopLeft (0, 2) list

[<EntryPoint>]
let main args = 
    let window = create 120 45 30
    window.content.Add (generateMaze 40 15 100000)
    mainLoop window
    0