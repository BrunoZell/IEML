module IEML

type Primitive = E | U | A | S | B | T

type Word<'PreviousLayerWord> = {
    Substance: 'PreviousLayerWord
    Attribute: 'PreviousLayerWord
    Mode: 'PreviousLayerWord
}

type L0Word = Word<Primitive>
type L1Word = Word<L0Word>
type L2Word = Word<L1Word>
type L3Word = Word<L2Word>
type L4Word = Word<L3Word>
type L5Word = Word<L4Word>
type L6Word = Word<L5Word>

type Word =
    | L0 of L0Word
    | L1 of L1Word
    | L2 of L2Word
    | L3 of L3Word
    | L4 of L4Word
    | L5 of L5Word
    | L6 of L6Word
