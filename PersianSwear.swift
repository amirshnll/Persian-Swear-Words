import Foundation
import Combine

// MARK: - PersianSwear Class

final class PersianSwear {
	
	typealias Words = Set<String>
	
	static let shared = PersianSwear(words: [])
	
	private(set) var words: Words = .init()
	
	init(words: Set<String> = []) {
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
				self.words = self.words.union(words)
				completion(.success(words))
			}
		}
	}
	
	func addWord(_ word: String) {
		words.insert(word)
	}
	
	func addWords(_ words: Set<String>) {
		self.words = self.words.union(words)
	}
	
	func addWords(_ words: [String]) {
		addWords(Set(words))
	}
	
	func removeWord(_ word: String) {
		words.remove(word)
	}
	
	func removeWords(_ words: Set<String>) {
		self.words = self.words.subtracting(words)
	}
	
	func removeWords(_ words: [String]) {
		removeWords(Set(words))
	}
	
	func isBadWord(_ word: String) -> Bool {
		return words.contains(word)
	}
	
	func hasBadWord(_ text: String) -> Bool {
		for word in text.components(separatedBy: .whitespacesAndNewlines) where !word.isEmpty {
			if words.contains(word) {
				return true
			}
		}
		return false
	}
	
	func badWords(in text: String) -> [String] {
		return text
			.components(separatedBy: " ")
			.filter(isBadWord(_:))
	}
	
	func replaceBadWords(in text: String, with replacementText: String = "*") -> String {
		return text
			.components(separatedBy: " ")
			.map { word in isBadWord(word) ? replacementText : word }
			.joined(separator: " ")
	}
	
}

// MARK: PersianSwearDataLoader Protocol and Implementation(s)

protocol PersianSwearDataLoader {
	func loadWords(_ completion: @escaping (_ result: Result<PersianSwear.Words, Error>) -> Void)
}

class GithubPersianSwearDataLoader: PersianSwearDataLoader {
	private let url = URL(string: "https://github.com/amirshnll/Persian-Swear-Words/raw/master/data.json")!
	
	init() {
		
	}
	
	func loadWords(_ completion: @escaping (Result<PersianSwear.Words, Error>) -> Void) {
		URLSession.shared.dataTask(with: URLRequest(url: url)) { data, response, error in
			if let error {
				completion(.failure(error))
				return
			}
			
			guard let data else { return }
			
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