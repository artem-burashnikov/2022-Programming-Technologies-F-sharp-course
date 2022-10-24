namespace HomeWork3

open System.Collections.Generic
open CLists
open Microsoft.FSharp.Core

module NTrees =

    /// This type represents an N-ary tree.
    // Each node in such tree may have any number of children.
    // Every node consists of its value and a list of the node's children which may be empty.
    type NTree<'Value> =
        | Leaf of value: 'Value
        | Node of parent: 'Value * children: CList<NTree<'Value>>



    /// General n-ary tree folding function.
    let rec fold folder acc tree =
        let recurse = fold folder

        match tree with
        | Leaf value -> folder acc value
        | Node (value, children) -> CLists.fold recurse (folder acc value) children



    /// Function makes a set of values in the nodes of an n-ary tree.
    // We pass hSetAdd as folder function to the general tree folding function.
    // Returns a count of unique elements.
    let countUniques tree =
        let hSetAdd (hSet: HashSet<'Value>) value =
            hSet.Add value |> ignore
            hSet

        let hSet = HashSet<'Value>()
        let result = fold hSetAdd hSet tree
        result.Count



    /// Function constructs a linked list of type CList from the values in the nodes of n-ary tree.
    // lstAdd is passed as a folder function to the general tree folding function.
    // Returns a CList that has all values.
    let mkCList tree =
        // The defined order of arguments (1st: cList; 2nd: elem) is important.
        // When this gets passed to fold as a folder function, the accumulator will be the first argument.
        let lstAdd cList elem = Cons(elem, cList)
        fold lstAdd Empty tree