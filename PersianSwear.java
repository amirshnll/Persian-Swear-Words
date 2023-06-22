import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class PersianSwear {
    private final ArrayList<String> swearWords = new ArrayList<>();

     public PersianSwear() {
         try (BufferedReader reader = new BufferedReader(new FileReader("data.txt"))) {
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

        return matcher.replaceAll("").trim()
                .replace("\u200c", " ")
                .replace("ي", "ی")
                .replace("ك", "ک")
                .replace("ـ", "");
    }

    public void addWord(String word) {
        swearWords.add(word.trim());
    }

    public void addWords(String[] words) {
        swearWords.addAll(List.of(words));
    }

    public void removeWord(String word) {
        swearWords.remove(word.trim());
    }

    public void removeWords(String[] words) {
        swearWords.removeAll(List.of(words));
    }

    public boolean isBad(String word) {
        return swearWords.contains(clearWord(word));
    }

    public boolean hasSwear(String text) {
        List<String> splitedText = List.of(text.split(" "));

         for (String word : splitedText)
             if (swearWords.contains(clearWord(word)))
                 return true;

        return false;
    }

    public String filterWords(String text, String symbol) {
        List<String> splitedText = new ArrayList<>(List.of(text.split(" ")));

        for (String word : splitedText)
            if (swearWords.contains(clearWord(word)))
                splitedText.set(splitedText.indexOf(word), symbol);

        return String.join(" ", splitedText);
    }

    public String filterWords(String text) {
         return filterWords(text, "*");
    }
}
