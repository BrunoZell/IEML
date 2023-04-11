module IEML

// ### Primitives ###

type Primitive = E | U | A | S | B | T

// ### Words ###

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

// ### Paradigms ###

type Paradigm = interface end // Todo

// ### Phrases ###

type NounInflection = NounInflection of Word
type VerbInflection = VerbInflection of Word

type NounPhrase = {
    Inflections: NounInflection Set
    Concept: Word
}

type VerbPhrase = {
    Inflections: VerbInflection Set
    Concept: Word
}

type Phrase = Accent * PhraseRoot * PhraseInteractant * PhraseInitiator * PhraseInteractant * PhraseRecipient
and Accent = Root | Initiator | Interactant | Recipient | Cause | Time | Place | Intention | Manner
and PhraseRoot = 
    | NounPhrase of NounPhrase
    | VerbPhrase of VerbPhrase
and WordOrPhrase = 
    | Word of Word
    | Phrase of Phrase
and PhraseInitiator = {
    Inflections: NounInflection Set
    Concept: WordOrPhrase
}
and PhraseInteractant = {
    Inflections: NounInflection Set
    Concept: WordOrPhrase
}
and PhraseRecipient = {
    Inflections: NounInflection Set
    Concept: WordOrPhrase
}
// Todo: Causality, Time, Place, Intention, Manner

// ### Declarations ###

/// @rootparadigm 
type RootParadigmDeclaration = {
    Type: RootParadigmType
    Domain: Paradigm
}
and RootParadigmType =
    | Category
    | Inflection
    | Auxilary
    | Junction

/// @inflection
type InflectionDeclaration = {
    Class: InflectionDeclarationClass
    Node: Word
}
and InflectionDeclarationClass = Verb | Noun

/// @auxilary
type AuxilaryDeclaration = {
    Role: AuxilaryDeclarationRole
    Node: Word
}
and AuxilaryDeclarationRole = Causation | Time | Place | Intention | Manner

/// @junction
type JunctionDeclaration = {
    Node: Word
}

/// @node
type NodeDeclaration = {
    Phrase: Phrase
}

type Declarations =
    | RootParadigm of RootParadigmDeclaration
    | Inflection of InflectionDeclaration
    | Auxilary of AuxilaryDeclaration
    | Junction of JunctionDeclaration
    | Node of NodeDeclaration
    | Paranode
    | Link
    | Function
