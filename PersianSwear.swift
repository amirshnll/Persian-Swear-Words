import Foundation

final class PersianSwear {
    private typealias Words = Set<String>
    
    static let shared = PersianSwear(words: [])
    
    private(set) var words: Words = Set<String>()
    
    init(words: Words = Set<String>()) {
        self.words = words
    }
    
    convenience init(words: [String] = []) {
        self.init(words: Set(words))
    }
    
    func loadWords(using loader: PersianSwearDataLoader, completion: @escaping (Result<Words, Error>) -> Void) {
        loader.loadWords { [weak self] result in
            guard let self = self else { return }
            
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let words):
                self.words = words
                completion(.success(words))
            }
        }
    }
    
    func addWord(_ word: String) {
        words.insert(word)
    }
    
    func addWords(_ words: Words) {
        self.words.formUnion(words)
    }
    
    func addWords(_ words: [String]) {
        addWords(Set(words))
    }
    
    func removeWord(_ word: String) {
        words.remove(word)
    }
    
    func removeWords(_ words: Words) {
        self.words.subtract(words)
    }
    
    func removeWords(_ words: [String]) {
        removeWords(Set(words))
    }
    
    func isBadWord(_ word: String) -> Bool {
        return words.contains(word)
    }
    
    func hasBadWord(_ text: String) -> Bool {
        let words = text.components(separatedBy: .whitespacesAndNewlines).filter { !$0.isEmpty }
        return words.contains { isBadWord($0) }
    }
    
    func badWords(in text: String) -> [String] {
        let words = text.components(separatedBy: " ")
        return words.filter(isBadWord)
    }
    
    func replaceBadWords(in text: String, with replacementText: String = "*") -> String {
        let mutableText = NSMutableString(string: text)
        let words = text.components(separatedBy: " ")
        
        words.forEach { word in
            if isBadWord(word) {
                mutableText.replaceOccurrences(of: word, with: replacementText)
            }
        }
        
        return mutableText as String
    }
}

protocol PersianSwearDataLoader {
    func loadWords(_ completion: @escaping (Result<PersianSwear.Words, Error>) -> Void)
}

class GithubPersianSwearDataLoader: PersianSwearDataLoader {
    private let url = URL(string: "https://github.com/amirshnll/Persian-Swear-Words/raw/master/data.json")!
    
    init() {
        
    }
    
    func loadWords(_ completion: @escaping (Result<PersianSwear.Words, Error>) -> Void) {
        URLSession.shared.dataTask(with: url) { data, response, error in
            if let error = error {
                completion(.failure(error))
                return
            }
            
            guard let data = data else { return }
            
            do {
                let model = try JSONDecoder().decode(Model.self, from: data)
                completion(.success(Set(model.word)))
            } catch {
                completion(.failure(error))
            }
        }.resume()
    }
    
    private struct Model: Codable {
        let word: [String]
    }
}