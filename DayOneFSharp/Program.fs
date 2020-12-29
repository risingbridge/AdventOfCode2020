// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    let inputArray = IO.File.ReadAllLines("./input.txt");
    printfn "Hello World from F#!"
    for i in 0 .. inputArray.Length - 1 do
        printfn inputArray[0]
    0 // return an integer exit code
