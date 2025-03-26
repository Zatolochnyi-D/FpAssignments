module FpAssignments.Window
open System
open System.Threading
open System.Collections.Generic
open Console
open SimpleMath
open GUI

type Binding = {
    key: ConsoleKey
    func: unit -> unit
}

type FragmentOrNothing = Fragment of Fragment | None of unit
type Window = private {
    dimensions: Vector
    sleepTime: int
    keyReader: unit -> ConsoleKey
    buffer: List<List<char>>
    content: List<Fragment>
    bindings: List<Binding>
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
    let sleepTime = fps |> double |> (/) 1000.0 |> roundToInt
    let dimensions = { x = width; y = height }
    { 
        dimensions = dimensions
        sleepTime = sleepTime
        keyReader = keyReader ()
        buffer = createEmptyBuffer dimensions
        content = List<Fragment> ()
        bindings = List<Binding> ()
    }

let addFragment window fragment =
    window.content.Add fragment

let addBinding window binding =
    window.bindings.Add binding

let clearBuffer window =
    for y = 0 to window.dimensions.y - 1 do
        for x = 0 to window.dimensions.x - 1 do
            window.buffer.[y].[x] <- ' '

let drawBuffer window =
    for y = 0 to window.dimensions.y - 1 do
        for x = 0 to window.dimensions.x - 1 do
            writeChar x y window.buffer.[y].[x]

// TODO: handle fragment go outside of the buffer
let writeFragmentToBuffer window fragment =
    let position = rectAbsolutePosition window.dimensions fragment.rect
    for y = 0 to fragment.rect.dimensions.y - 1 do
        for x = 0 to fragment.rect.dimensions.x - 1 do
            window.buffer.[position.y + y].[position.x + x] <- fragment.content.[y].[x]

let writeFragmentsToBuffer window =
    for fragment in window.content do
        writeFragmentToBuffer window fragment

let validateWindowSize window = 
    let currentWidth, currentHeight = consoleSize ()
    currentWidth = window.dimensions.x && currentHeight = window.dimensions.y

let writeWindowWrongSizeMessage window =
    let consoleDimensions = consoleSize () |> vectorFromTuple
    let content = $"Please, adjust console size to {window.dimensions.x}x{window.dimensions.y}\n\
                    (Current: {consoleDimensions.x}x{consoleDimensions.y})"
    let textBox = textBox Center Center Middle content
    let pos = rectAbsolutePosition consoleDimensions textBox.fragment.rect
    for y = 0 to textBox.fragment.content.Count - 1 do
        for x = 0 to textBox.fragment.content.[y].Count - 1 do
            writeChar (x + pos.x) (y + pos.y) textBox.fragment.content.[y].[x]

let rec mainLoop window : unit =
    Thread.Sleep window.sleepTime
    clear ()
    clearBuffer window
    let key = window.keyReader ()

    if validateWindowSize window then
        for binding in window.bindings do
            if key = binding.key then binding.func ()
        writeFragmentsToBuffer window
        drawBuffer window
    else 
        writeWindowWrongSizeMessage window
    mainLoop window    