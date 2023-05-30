module IEML

// ### Semantic Primitives ###

type Primitive = E | U | A | S | B | T

// ### Full Words ###

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

// Todo: Add validation to only allow inflections for either verbal or nominal as defined by the dictionary.
// https://intlekt.io/paradigms-of-inflections/

/// Represented by full words in the dictionary or by subordinate sentences – are preceded by a hash #.
type Concept = 
    | Word of Word
    | Phrase of Phrase

and Phrase = {
    Accent: SemanticAccent
    Root: RootRole
    Initiator: ActorRole
    Interactant: ActorRole
    Recipient: ActorRole
    Causality: QualityRole
    Time: QualityRole
    Place: QualityRole
    Intention: QualityRole
    Manner: QualityRole
}

and SemanticAccent = Root | Initiator | Interactant | Recipient | Causality | Time | Place | Intention | Manner

/// For phrase role: 0 root
and RootRole = {
    Concept: Concept
    Inflections: Word Set
    Referent: string option
}

/// For phrase roles: 1 initiator, 2 interactant, 3 recipient
and ActorRole = {
    Concept: Concept
    Inflections: Word Set
    Referent: string option
}

/// For phrase roles: 4 causality, 5 time, 6 place, 7 intention, 8 manner
and QualityRole = {
    Concept: Concept
    Inflections: Word Set
    Prepositions: Word Set
    Referent: string option
}

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
