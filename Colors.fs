module FpAssignments.Colors
open System

type Color =
    | SingleColor of ConsoleColor
let black = SingleColor ConsoleColor.Black
let darkBlue = SingleColor ConsoleColor.DarkBlue
let darkGreen = SingleColor ConsoleColor.DarkGreen
let darkCyan = SingleColor ConsoleColor.DarkCyan
let darkRed = SingleColor ConsoleColor.DarkRed
let darkMagenta = SingleColor ConsoleColor.DarkMagenta
let darkYellow = SingleColor ConsoleColor.DarkYellow
let gray = SingleColor ConsoleColor.Gray
let darkGray = SingleColor ConsoleColor.DarkGray
let blue = SingleColor ConsoleColor.Blue
let green = SingleColor ConsoleColor.Green
let cyan = SingleColor ConsoleColor.Cyan
let red = SingleColor ConsoleColor.Red
let magenta = SingleColor ConsoleColor.Magenta
let yellow = SingleColor ConsoleColor.Yellow
let white = SingleColor ConsoleColor.White

let unpackColor color =
    match color with
        | SingleColor x -> x