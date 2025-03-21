module FpAssignments.Window

open System.Threading
open System.Collections.Generic
open Console
open SimpleMath
open FpAssignments.GUI

type Window = {
    width: int
    height: int
    sleepTime: int
    content: List<DrawRect>
}

let private writeWindowWrongSizeMessage window =
    let width, height = consoleSize ()
    let firstLine = $"Please, make console size {window.width}x{window.height}"
    let secondLine = $"(Current: {width}x{height})"
    let firstLineOffset = center firstLine.Length
    let secondLineOffset = center secondLine.Length
    let x, y = width / 2, height / 2
    writeText (x - firstLineOffset) y firstLine
    writeText (x - secondLineOffset) (y + 1) secondLine

let transformCoordinatesToWindow window anchor (x: int) (y: int) =
     match anchor with
        | TopLeft -> x, y
        | TopCenter -> window.width / 2 + x, y

let private validateWindowSize window = 
    let currentWidth, currentHeight = consoleSize ()
    currentWidth = window.width && currentHeight = window.height

let drawRect window (rect: DrawRect) =
    // let anchorX, anchorY = transformCoordinatesToWindow window rect.anchor rect.x rect.y
    let x, y = rect.position
    let count = counter 0
    for line in rect.content do
        writeText x (y + count ()) line 
        
let rec mainLoop window : unit =
    Thread.Sleep window.sleepTime
    clear()

    if not <| validateWindowSize window then
        writeWindowWrongSizeMessage window
        mainLoop window
    else
        for rect in window.content do
            drawRect window rect
        mainLoop window

let create x y fps =
    let sleepTime = fps |> double |> (/) 1000.0 |> roundToInt
    { width = x; height = y; sleepTime = sleepTime; content = List<DrawRect> ()}