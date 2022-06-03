import java.util.*;

class LinesInfo {
    public ArrayList<Integer> lines;
    public int size;
    public int width;

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
    public int pos;
    public String value;

    public Note(int pos, String value)
    {
        this.pos = pos;
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
        // find where note lines are, size of place for notes and width of a note line 
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
                    // line found
                    if (info.width == 0) {
                        info.width = lineEnd - lineBegin;
                    }
                    int currentLine = lineBegin + info.width / 2;
                    if (info.lines.size() > 0) {
                        // add a line and a place for notes between this line and previous one
                        int previousLine = info.lines.get(info.lines.size() - 1);
                        if (info.size == 0)
                            info.size = currentLine - previousLine; 
                        info.lines.add((int)Math.round((double)(previousLine + currentLine) / 2));
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
        info.lines.add(0, info.lines.get(0) - info.size / 2);
        info.lines.add(info.lines.get(9) + info.size / 2);
        info.lines.add(info.lines.get(9) + info.size);
        return info;
    }

    private static boolean check4Cross(boolean[][] image, int x, int y, int w, int h, boolean type) {
        // Check if we cross note border at point (x,y). We cross it if there's a point of given type (black or white) around
        for (int i=x-1; i<=x+1; i++) {
            for (int j=y-1; j<=y+1; j++) {
                if (i<0 || i>=w || j<0 || j>=h)
                    continue;
                if (image[i][j] == type)
                    return true;
            }
        }
        return false;
    }

    private static String scan (boolean[][] image, int x, int y, int w, int h, LinesInfo info) {
        // scan from current point in 8 directions
        boolean[] dirCheck = new boolean[8];
        double[] switchd = new double[8];
        boolean center = image[x][y];
        int[][] delta = new int[][] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
        int s = (int)Math.round((double)info.size/2);
        int sd = s - (info.width / 2 + 1);
        for (int r =0; r<=s+2; r++) {
            if (r == 0) {
                for (int i=0; i<8; i++) {
                    dirCheck[i] = true;
                }
            } else {
                for (int d=0; d<8; d++) {
                    // if we cross border of note or if w
                    boolean cross = 
                        x+r*delta[d][0] >= 0 && x+r*delta[d][0] < w &&
                        y+r*delta[d][1] >= 0 && y+r*delta[d][1] < h &&
                        x+r*delta[d][0] >= x-sd && x+r*delta[d][0] <= x+sd &&
                        y+r*delta[d][1] >= y-sd && y+r*delta[d][1] <= y+sd
                        ? check4Cross(image, x+r*delta[d][0], y+r*delta[d][1], w, h, !center) : true;
                    if (dirCheck[d] && cross) {
                        // if crossing is found store its distance from current point
                        switchd[d] = Math.sqrt(Math.pow(r * delta[d][0], 2) + Math.pow(r*delta[d][1],2));
                        dirCheck[d] = false;
                    }
                }
            }
        }
        boolean allCrosses = true;
        double mind = s+1;
        double maxd = 0;
        // check if we crossed some border in all 8 directions and distance from (x,y) to crossing points are roughly equal
        // if so, (x,y) is the center of the round note
        for (int i=0; i < 8; i++) {
            if (switchd[i] < mind)
                mind = switchd[i];
            if (switchd[i] > maxd) {
                maxd = switchd[i];
            }
            if (mind <= info.width || maxd - mind > 2) {
                allCrosses = false;
                break;
            }
        }
        // we're not in the center of the note
        if (!allCrosses) return "";
        // determine note type based on whether (x,y) is black (Q) or white(H)
        return center ? "Q" : "H";
    }

    public static String getNotes(int w, int h, String encodedImage) {
        boolean[][] image = decodeImage(w, h, encodedImage);
        LinesInfo lineInfo = getLines(w, h, image);
        String noteTypes = "GFEDCBAGFEDC";
        NoteList notes = new NoteList(lineInfo.size);
        // the idea is to scan along found lines and between them to find notes
        for (int p = 0; p < w; p++) {
            for (int l = 0; l < lineInfo.lines.size(); l++) {
                String noteLen = scan(image, p, lineInfo.lines.get(l), w, h, lineInfo);
                if (noteLen == "H" || noteLen == "Q") {
                    Note newNote = new Note(p, Character.toString(noteTypes.charAt(l)).concat(noteLen));
                    notes.add(newNote);
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

        String notes = getNotes(w, h, image);

        System.out.println(notes);
    }
}
