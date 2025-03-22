module FpAssignments.Window

open System.Threading
open System.Collections.Generic
open Console
open SimpleMath
open GUI

type Window = private {
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

let private validateWindowSize window = 
    let currentWidth, currentHeight = consoleSize ()
    currentWidth = window.width && currentHeight = window.height

let private rectAbsolutePosition window rect =
    let rectLocalTopLeftPosition = rectTopLeftPosition rect
    let x, y = rect.position
    let globalPosition = 
        match rect.anchor with
        | TopLeft -> x, y
        | TopCenter -> center window.width + x, y
    addTuples globalPosition rectLocalTopLeftPosition

let private drawRect window (rect: DrawRect) =
    let x, y = rectAbsolutePosition window rect
    let count = counter 0
    setBackground rect.backgroundColor
    setForeground rect.foregroundColor
    for line in rect.content do
        writeText x (y + count ()) line 

let create x y fps =
    hideCursor ()
    let sleepTime = fps |> double |> (/) 1000.0 |> roundToInt
    { width = x; height = y; sleepTime = sleepTime; content = List<DrawRect> ()}

let addContent window drawRect = 
    window.content.Add drawRect

let windowSize window = 
    window.width, window.height

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