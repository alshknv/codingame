import java.util.*;

class LinesInfo {
    public ArrayList<Integer> lines;
    public int lineHeight;
    public int lineWidth;

    public LinesInfo() {
        lines = new ArrayList<Integer>();
    }
}

class NoteList {
    private ArrayList<Note> notes;
    private int size;

    public NoteList(int size) {
        this.size = size;
        this.notes = new ArrayList<Note>();
    }

    public void add(Note note) {
        for (int i=0; i<notes.size(); i++)
            if (Math.abs(notes.get(i).pos - note.pos) <= size)
                return;
        notes.add(note);
    }

    public ArrayList<String> get() {
        ArrayList<String> stringNotes = new ArrayList<String>();
        for (int i=0; i<notes.size(); i++)
            stringNotes.add(notes.get(i).value);
        return stringNotes;
    }
}

class Note {
    public int lineIndex;
    public int pos;
    public int posIndex;
    public String value;

    public Note(int lineIndex, int pos, int posIndex, String value)
    {
        this.lineIndex = lineIndex;
        this.pos = pos;
        this.posIndex = posIndex;
        this.value = value;
    }
}

class NoteLine {
    public int begin;
    public int end;
    public NoteLine(int begin, int end) {
        this.begin = begin;
        this.end = end;
    }
}

class Solution {

    private static boolean[][] decodeImage(int w, int h, String encodedImage) {
        boolean[][] decodedImage = new boolean[w][h];
        int imgPos = 0;
        String[] imgData = encodedImage.split(" ");
        for (int i = 0; i < imgData.length; i += 2) {
            Boolean bit = imgData[i].equalsIgnoreCase("B");
            int number = Integer.parseInt(imgData[i + 1]);
            for (int j = 0; j < number; j++) {
                if (imgPos / w < h)
                    decodedImage[imgPos % w][imgPos / w] = bit;
                imgPos++;
            }
        }
        return decodedImage;
    }

    private static LinesInfo getLines(int w, int h, boolean[][] image) {
        LinesInfo info = new LinesInfo();
        for (int i = 0; i < w; i++) {
            Boolean isBlack = false;
            int lineBegin = -1, lineEnd = -1;
            for (int j = 0; j < h; j++) {
                if (image[i][j] && !isBlack) {
                    isBlack = true;
                    lineBegin = j;
                } else if (!image[i][j] && isBlack) {
                    isBlack = false;
                    lineEnd = j;
                }
                if (lineBegin > 0 && lineEnd > 0) {
                    if (info.lineWidth == 0) {
                        info.lineWidth = lineEnd - lineBegin;
                    }
                    int currentLine = lineBegin + info.lineWidth / 2;
                    if (info.lines.size() > 0) {
                        int previousLine = info.lines.get(info.lines.size() - 1);
                        if (info.lineHeight == 0)
                            info.lineHeight = currentLine - previousLine; // - width;
                        info.lines.add((previousLine + currentLine) / 2);
                    }
                    info.lines.add(currentLine);
                    lineBegin = lineEnd = -1;
                }
            }
            if (info.lines.size() == 9)
                break;
            info.lines.clear();
        }
        // additional places for notes above first line and below last
        info.lines.add(0, info.lines.get(0) - info.lineHeight / 2);
        info.lines.add(info.lines.get(9) + info.lineHeight / 2);
        info.lines.add(info.lines.get(9) + info.lineHeight);
        return info;
    }

    private static String scanVertically(boolean[][] image, int line, LinesInfo lineInfo, int h, int x) {
        ArrayList<NoteLine> linesCrossed = new ArrayList<NoteLine>();
        int lineBegin = 0;
        boolean isLine = false;
        for (int i= line - lineInfo.lineHeight / 2 + lineInfo.lineWidth / 2; i < line + lineInfo.lineHeight / 2 - lineInfo.lineWidth / 2; i++) {
            if (i<0 || i>h || Math.abs(i-line) <= lineInfo.lineWidth / 2)
                continue;
            if (!isLine && image[x][i]) {
                isLine = true;
                lineBegin = i;
            }
            if (isLine && (!image[x][i] || i == line + lineInfo.lineHeight / 2 - lineInfo.lineWidth / 2 - 1)) {
                isLine = false;
                linesCrossed.add(new NoteLine(lineBegin, i));
            }
        }
        
        if (linesCrossed.size() == 1 && linesCrossed.get(0).begin < line && linesCrossed.get(0).end > line) return "Q";
        if (linesCrossed.size() == 2 && linesCrossed.get(0).begin < line && linesCrossed.get(0).end<line &&
            linesCrossed.get(1).begin > line && linesCrossed.get(1).end > line) return "H";
        return "";
    }

    private static String CheckScan(ArrayList<String> scan) {
        String first = scan.get(0);
        for (int i=1; i<scan.size(); i++) {
            if (scan.get(i) != first) return "";
        }
        return first;
    }

    public static String getNotes(int w, int h, String encodedImage) {
        boolean[][] image = decodeImage(w, h, encodedImage);
        LinesInfo lineInfo = getLines(w, h, image);
        String noteTypes = "GFEDCBAGFEDC";
        NoteList notes = new NoteList(lineInfo.lineHeight);
        ArrayList<ArrayList<String>> scan = new ArrayList<ArrayList<String>>();
        for (int i=0;i<lineInfo.lines.size(); i++) {
            scan.add(new ArrayList<String>());
        }
        for (int p = 0; p < w; p++) {
            for (int l = 0; l < lineInfo.lines.size(); l++) {
                scan.get(l).add(scanVertically(image, lineInfo.lines.get(l), lineInfo, h, p));
                if (scan.get(l).size() > lineInfo.lineHeight - lineInfo.lineWidth * 4) {
                    scan.get(l).remove(0);
                    String noteLen = CheckScan(scan.get(l));
                    if (noteLen == "H" || noteLen == "Q") {
                        Note newNote = new Note(l, p, p / lineInfo.lineHeight,
                                Character.toString(noteTypes.charAt(l)).concat(noteLen));
                        notes.add(newNote);
                    }
                }
            }
        }
        return String.join(" ", notes.get());
    }

    public static void main(String args[]) {
        Scanner in = new Scanner(System.in);
        int w = in.nextInt();
        int h = in.nextInt();
        if (in.hasNextLine()) {
            in.nextLine();
        }
        String image = in.nextLine();
        in.close();
        // Write an answer using System.out.println()
        // To debug: System.err.println("Debug messages...");
        String notes = getNotes(w, h, image);
        System.out.println(notes);
    }
}
