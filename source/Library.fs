module IEML

// ### Semantic Primitives ###

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

// Todo: Add validation to only allow inflections for either verbal or nominal as defined by the dictionary.
// https://intlekt.io/paradigms-of-inflections/

/// Represented by full words in the dictionary or by subordinate sentences – are preceded by a hash #.
type Concept = 
    | Word of Word
    | Phrase of Phrase

/// A parsed @node gets serialized into this abstract representation of the phrase
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

// ### Dictionary ###

// As interpreted from a set of declarations

type Dictionary = {
    /// Set of all @inflection declarations
    Inflections: InflectionSet

    /// Set of all @auxilary declarations
    Auxilaries: AuxilarySet

    /// Set of all @junction declarations
    /// Set of all @rootparadigm declarations
    /// Set of all @node declarations
}
and InflectionSet = {
    /// All @inflection declarations with 'class: verb', only to be used with ~verb
    Verbs: Word Set
    
    /// All @inflection declarations with 'class: noun', only to be used with ~noun
    Nouns: Word Set
}
and AuxilarySet = {
    /// All @auxilary declarations with 'role: 4', only to be used in role 4
    Causality: Word Set
    
    /// All @auxilary declarations with 'role: 5', only to be used in role 5
    Time: Word Set
    
    /// All @auxilary declarations with 'role: 6', only to be used in role 6
    Place: Word Set
    
    /// All @auxilary declarations with 'role: 7', only to be used in role 7
    Intention: Word Set
    
    /// All @auxilary declarations with 'role: 8', only to be used in role 8
    Manner: Word Set
}

// ### Parser tokens for declarations ###

// As read from a valid IEML text file

/// Describes paradigm literals in the form of '"A:.m.O:M:.-".'
type ParadigmLiteral = ParadigmSelector of string

/// Describes word literals in the form of '"A:.m.y.-".'
type WordLiteral = WordSelector of string

/// Describes phrase literals in the form of '(0 ~noun #"E:").'
type PhraseLiteral = PhraseLiteral of string

/// @rootparadigm
type RootParadigmDeclaration = {
    Type: RootParadigmType
    Paradigm: ParadigmLiteral
}
and RootParadigmType =
    | Category
    | Inflection
    | Auxilary
    | Junction

/// @inflection
type InflectionDeclaration = {
    Class: InflectionDeclarationClass
    Word: WordLiteral
}
and InflectionDeclarationClass = Verb | Noun

/// @auxilary
type AuxilaryDeclaration = {
    Role: AuxilaryDeclarationRole
    Word: WordLiteral
}
and AuxilaryDeclarationRole = Casuality | Time | Place | Intention | Manner

/// @junction
type JunctionDeclaration = {
    Word: WordLiteral
}

/// @node
type NodeDeclaration = {
    Phrase: PhraseLiteral
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
