module FpAssignments.Window
open System.Threading
open System.Collections.Generic
open Console
open SimpleMath
open GUI

type Window = private {
    dimensions: Vector
    sleepTime: int
    buffer: List<List<char>>
    content: List<DrawRect>
}

let createEmptyBuffer dimensions =
    let buffer = List<List<char>> ()
    for y = 0 to dimensions.y - 1 do
        List<char> () |> buffer.Add
        for _ = 0 to dimensions.x - 1 do
            buffer.[y].Add ' ';
    buffer

let create width height fps =
    hideCursor ()
    preventInputPrinting ()
    let sleepTime = fps |> double |> (/) 1000.0 |> roundToInt
    let dimensions = { x = width; y = height }
    { dimensions = dimensions; sleepTime = sleepTime; buffer = createEmptyBuffer dimensions; content = List<DrawRect> ()}

let clearBuffer window =
    for y = 0 to window.dimensions.y - 1 do
        for x = 0 to window.dimensions.x - 1 do
            window.buffer.[y].[x] <- ' '

let drawBuffer window =
    for y = 0 to window.dimensions.y - 1 do
        for x = 0 to window.dimensions.x - 1 do
            writeChar x y window.buffer.[y].[x]

let validateWindowSize window = 
    let currentWidth, currentHeight = consoleSize ()
    currentWidth = window.dimensions.x && currentHeight = window.dimensions.y

let writeWindowWrongSizeMessage window =
    let width, height = consoleSize ()
    let firstLine = $"Please, make console size {window.dimensions.x}x{window.dimensions.y}"
    let secondLine = $"(Current: {width}x{height})"
    let firstLineOffset = center firstLine.Length
    let secondLineOffset = center secondLine.Length
    let x, y = width / 2, height / 2
    writeString (x - firstLineOffset) y firstLine
    writeString (x - secondLineOffset) (y + 1) secondLine

let rec mainLoop window : unit =
    Thread.Sleep window.sleepTime
    clear ()
    clearBuffer window

    // logic here. Listen to keys, fill the buffer.
    if validateWindowSize window then
        // common logic
        drawBuffer window
        mainLoop window
    else 
        writeWindowWrongSizeMessage window
        mainLoop window    

// let private drawRect window rect =
//     let x, y = rect.position
//     let width, height = rect.dimensions
//     for contentY = 0 to height - 1 do
//         for contentX = 0 to width - 1 do
//             let square = rect.content.[contentY].[contentX]
//             writeChar (x + contentX) (y + contentY) square.char

// let addContent window drawRect = 
//     window.content.Add drawRect