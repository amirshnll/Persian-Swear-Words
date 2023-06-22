import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class PersianSwear {

  private final List<String> swearWords = new ArrayList<>();

  public PersianSwear() {
    try (
      BufferedReader reader = new BufferedReader(new FileReader("data.txt"))
    ) {
      String line;
      while ((line = reader.readLine()) != null) {
        String[] splitedWords = line.split(", ");
        swearWords.addAll(List.of(splitedWords));
      }
    } catch (IOException e) {
      throw new RuntimeException(e);
    }
  }

  private String clearWord(String word) {
    Pattern pattern = Pattern.compile("[\\p{P}\\p{Mn}\\p{S}\\p{javaDigit}]");
    Matcher matcher = pattern.matcher(word);
    matcher.reset();

    return matcher
      .replaceAll("")
      .trim()
      .replace("\u200c", " ")
      .replace("ي", "ی")
      .replace("ك", "ک")
      .replace("ـ", "");
  }

  public void addWord(String word) {
    swearWords.add(word.trim());
  }

  public void addWords(String... words) {
    swearWords.addAll(List.of(words));
  }

  public void removeWord(String word) {
    swearWords.remove(word.trim());
  }

  public void removeWords(String... words) {
    swearWords.removeAll(List.of(words));
  }

  public boolean isBad(String word) {
    return swearWords.contains(clearWord(word));
  }

  public boolean hasSwear(String text) {
    List<String> splitText = List.of(text.split(" "));

    for (String word : splitText) {
      if (swearWords.contains(clearWord(word))) {
        return true;
      }
    }

    return false;
  }

  public String filterWords(String text, String symbol) {
    List<String> splitText = new ArrayList<>(List.of(text.split(" ")));

    for (int i = 0; i < splitText.size(); i++) {
      String word = splitText.get(i);
      if (swearWords.contains(clearWord(word))) {
        splitText.set(i, symbol);
      }
    }

    return String.join(" ", splitText);
  }

  public String filterWords(String text) {
    return filterWords(text, "*");
  }
}
