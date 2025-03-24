module FpAssignments.MazeGenerator

// open System
// open System.Collections.Generic
// open System.Linq
// open SimpleMath
// open GUI
// open Colors

// type NodeType = Entrance | Exit | Maze
// type Node = { x: int; y: int; neighbors: List<Node>; nodeType: NodeType}
// type Maze = { width: int; height: int; nodes: List<List<Node>> }

// let randomDirection maze (random: Random) x y =
//     let allowedDirections = List<int * int>()
//     if x = 0 |> not  then allowedDirections.Add (-1, 0) 
//     if x = maze.width - 1 |> not then allowedDirections.Add (1, 0) 
//     if y = 0 |> not then allowedDirections.Add (0, -1) 
//     if y = maze.height - 1 |> not then allowedDirections.Add (0, 1) 
//     allowedDirections.[random.Next allowedDirections.Count]

// let fillBaseMaze maze = 
//     for _ = 0 to maze.height - 1 do  
//         maze.nodes.Add (List<Node>())
//     maze.nodes.[0].Add { x = 0; y = 0; neighbors = List<Node> (); nodeType = Entrance }
//     for y = 1 to maze.height - 1 do
//         let node = { x = 0; y = y; neighbors = List<Node> (); nodeType = Maze }
//         node.neighbors.Add maze.nodes.[y - 1].[0]
//         maze.nodes.[y].Add node
//     for y = 0 to maze.height - 1 do  
//         for x = 1 to maze.width - 1 do
//             let node = { x = x; y = y; neighbors = List<Node> (); nodeType = Maze}
//             node.neighbors.Add maze.nodes.[y].[x - 1]
//             maze.nodes.[y].Add node
//     let x, y = maze.width - 1, maze.height - 1
//     let node = { x = x; y = y; neighbors = List<Node> (); nodeType = Exit}
//     node.neighbors.Add maze.nodes.[y].[x - 1]
//     maze.nodes.[y].[x] <- node
//     maze

// let randomizeMaze iterations maze =
//     let r = Random ()
//     let rec recursiveRandomizer iterations currentInteration maze rootX rootY =
//         if currentInteration <> iterations then
//             let shiftX, shiftY = randomDirection maze r rootX rootY
//             let x, y = rootX + shiftX, rootY + shiftY
//             let newRoot = maze.nodes.[y].[x]
//             newRoot.neighbors.Clear ()
//             let oldRoot = maze.nodes.[rootY].[rootX]
//             oldRoot.neighbors.Add newRoot
//             recursiveRandomizer iterations (currentInteration + 1) maze x y
//         else
//             maze
//     recursiveRandomizer iterations 0 maze 0 0

// let createBothSidesConnections maze = 
//     for line in maze.nodes do
//         for node in line do
//             for neighbor in node.neighbors do
//                 neighbor.neighbors.Add node
//     for line in maze.nodes do
//         for node in line do
//             let distinctNeighbors = (node.neighbors.Distinct()).ToList()
//             node.neighbors.Clear ()
//             node.neighbors.AddRange distinctNeighbors
//     maze

// let mazeTile nodeType index =
//     let w = { char = '#'; fgColor = white; bgColor = white } // wall
//     let s = { char = '0'; fgColor = gray; bgColor = black } // start
//     let e = { char = '['; fgColor = gray; bgColor = black } // start
//     let o = { char = ' '; fgColor = black; bgColor = black } // open
//     let x = { char = 'X'; fgColor = red; bgColor = darkRed } // error
//     let tile =
//         match nodeType with
//         | Entrance -> 
//             match index with
//             | 0b0010 -> [w; s; w;
//                          w; o; o;
//                          w; w; w]
//             | 0b0100 -> [w; s; w;
//                          w; o; w;
//                          w; o; w]
//             | 0b0110 -> [w; s; w;
//                          w; o; o;
//                          w; o; w]
//             | _ ->      [x; x; x;
//                          x; x; x;
//                          x; x; x]
//         | Exit ->
//             match index with
//             | 0b0001 -> [w; o; w;
//                          w; o; w;
//                          w; e; w]
//             | 0b1000 -> [w; w; w;
//                          o; o; w;
//                          w; e; w]
//             | 0b1001 -> [w; o; w;
//                          o; o; w;
//                          w; e; w]
//             | _ ->      [x; x; x;
//                          x; x; x;
//                          x; x; x]
//         | Maze ->
//             match index with
//             | 0b0001 -> [w; o; w;
//                          w; o; w;
//                          w; w; w]
//             | 0b0010 -> [w; w; w;
//                          w; o; o;
//                          w; w; w]
//             | 0b0100 -> [w; w; w;
//                          w; o; w;
//                          w; o; w]
//             | 0b1000 -> [w; w; w;
//                          o; o; w;
//                          w; w; w]
//             | 0b0011 -> [w; o; w;
//                          w; o; o;
//                          w; w; w]
//             | 0b1001 -> [w; o; w;
//                          o; o; w;
//                          w; w; w]
//             | 0b0110 -> [w; w; w;
//                          w; o; o;
//                          w; o; w]
//             | 0b1100 -> [w; w; w;
//                          o; o; w;
//                          w; o; w]
//             | 0b0101 -> [w; o; w;
//                          w; o; w;
//                          w; o; w]
//             | 0b1010 -> [w; w; w;
//                          o; o; o;
//                          w; w; w]
//             | 0b1011 -> [w; o; w;
//                          o; o; o;
//                          w; w; w]
//             | 0b1110 -> [w; w; w;
//                          o; o; o;
//                          w; o; w]
//             | 0b0111 -> [w; o; w;
//                          w; o; o;
//                          w; o; w]
//             | 0b1101 -> [w; o; w;
//                          o; o; w;
//                          w; o; w]
//             | 0b1111 -> [w; o; w;
//                          o; o; o;
//                          w; o; w]
//             | _ ->      [x; x; x;
//                          x; x; x;
//                          x; x; x]
//     List<Square> tile

// let generateRect window maze =
//     let content = List<List<Square>> ()
//     for _ in maze.nodes do
//         List<Square> () |> content.Add
//         List<Square> () |> content.Add
//         List<Square> () |> content.Add
//     let yCount = counter 0
//     for line in maze.nodes do
//         let y = yCount ()
//         for node in line do
//             let sides = sum ()
//             for neighbor in node.neighbors do
//                 let i =
//                     match node.x - neighbor.x, node.y - neighbor.y with
//                     | 0, 1 -> 0b0001 //0b_left_bottom_right_top
//                     | -1, 0 -> 0b0010
//                     | 0, -1 -> 0b0100
//                     | 1, 0 -> 0b1000
//                     | _ -> 0b0000
//                 sides i |> ignore
//             let tile = 0 |> sides |> mazeTile node.nodeType
//             for i = 0 to tile.Count / 3 - 1 do
//                 tile.Slice (3 * i, 3) |> content.[y * 3 + i].AddRange
        
//     drawRectFromSquaresList (0, 0) content

// let generateMaze window width height iterations = 
//     let maze = { width = width; height = height; nodes = List<List<Node>>()}
//     fillBaseMaze maze |> randomizeMaze iterations |> createBothSidesConnections |> generateRect window
            