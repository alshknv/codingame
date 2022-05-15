import static org.junit.Assert.assertTrue;

import org.junit.Assert;
import org.junit.Test;

public class AppTest 
{
    @Test
    public void oneQuarterNoteBetweenLines()
    {
        Assert.assertEquals("AQ", Solution.getNotes(120, 176,
                "W 4090 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 1040 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 58 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 80 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 58 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 66 B 10 W 4 B 2 W 103 B 12 W 3 B 2 W 101 B 16 W 1 B 2 W 101 B 16 W 1 B 2 W 100 B 20 W 99 B 21 W 99 B 21 W 98 B 22 W 98 B 22 W 98 B 22 W 98 B 22 W 98 B 22 W 98 B 22 W 98 B 22 W 99 B 20 W 100 B 20 W 101 B 18 W 103 B 16 W 104 B 16 W 106 B 12 W 63 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 2420 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 5050")
        );
    }

    @Test
    public void oneQuarterNoteOnALine() {
        Assert.assertEquals("BQ", Solution.getNotes(120, 176,
                "W 4090 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 80 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 58 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 80 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 104 B 10 W 4 B 2 W 103 B 12 W 3 B 2 W 101 B 16 W 1 B 2 W 101 B 16 W 1 B 2 W 100 B 20 W 99 B 21 W 99 B 21 W 98 B 22 W 58 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 60 B 22 W 98 B 22 W 99 B 20 W 100 B 20 W 101 B 18 W 103 B 16 W 104 B 16 W 106 B 12 W 109 B 10 W 1384 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 2420 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 5050"));
    }

    @Test
    public void oneHalfNoteBetweenLines() {
        Assert.assertEquals("AH", Solution.getNotes(120, 176,
                "W 4090 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 1040 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 58 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 80 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 118 B 2 W 58 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 66 B 10 W 4 B 2 W 103 B 12 W 3 B 2 W 101 B 4 W 8 B 4 W 1 B 2 W 101 B 2 W 12 B 2 W 1 B 2 W 100 B 2 W 14 B 4 W 99 B 3 W 14 B 4 W 99 B 2 W 16 B 3 W 98 B 3 W 16 B 3 W 98 B 2 W 18 B 2 W 98 B 2 W 18 B 2 W 98 B 2 W 18 B 2 W 98 B 2 W 18 B 2 W 98 B 2 W 18 B 2 W 98 B 3 W 16 B 3 W 99 B 2 W 16 B 2 W 100 B 3 W 14 B 3 W 101 B 2 W 14 B 2 W 103 B 2 W 12 B 2 W 104 B 4 W 8 B 4 W 106 B 12 W 63 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 2420 B 100 W 20 B 100 W 20 B 100 W 20 B 100 W 5050"));
    }

    @Test
    public void onlyQuarterNotesWithoutLowerC() {
        Assert.assertEquals("CQ CQ CQ CQ DQ EQ CQ AQ", Solution.getNotes(420, 176,
                "W 14290 B 400 W 20 B 400 W 20 B 400 W 20 B 400 W 256 B 10 W 409 B 12 W 406 B 16 W 404 B 16 W 403 B 18 W 401 B 20 W 400 B 20 W 399 B 22 W 398 B 22 W 82 B 2 W 314 B 22 W 82 B 2 W 314 B 22 W 82 B 2 W 314 B 22 W 82 B 2 W 278 B 10 W 26 B 22 W 82 B 2 W 277 B 12 W 25 B 22 W 82 B 2 W 275 B 16 W 23 B 21 W 83 B 2 W 275 B 16 W 23 B 21 W 83 B 2 W 274 B 18 W 22 B 20 W 84 B 2 W 273 B 20 W 21 B 2 W 1 B 16 W 85 B 2 W 273 B 20 W 21 B 2 W 1 B 16 W 85 B 2 W 272 B 22 W 20 B 2 W 3 B 12 W 87 B 2 W 84 B 400 W 20 B 400 W 20 B 400 W 20 B 400 W 46 B 10 W 32 B 10 W 32 B 10 W 32 B 10 W 26 B 22 W 20 B 2 W 46 B 10 W 46 B 2 W 109 B 12 W 30 B 12 W 30 B 12 W 30 B 12 W 25 B 22 W 20 B 2 W 45 B 12 W 45 B 2 W 107 B 16 W 26 B 16 W 26 B 16 W 26 B 16 W 23 B 21 W 21 B 2 W 43 B 16 W 43 B 2 W 107 B 16 W 26 B 16 W 26 B 16 W 26 B 16 W 23 B 21 W 21 B 2 W 43 B 16 W 43 B 2 W 106 B 18 W 24 B 18 W 24 B 18 W 24 B 18 W 22 B 20 W 22 B 2 W 42 B 18 W 42 B 2 W 105 B 20 W 22 B 20 W 22 B 20 W 22 B 20 W 21 B 2 W 1 B 16 W 23 B 2 W 41 B 20 W 41 B 2 W 105 B 20 W 22 B 20 W 22 B 20 W 22 B 20 W 21 B 2 W 1 B 16 W 23 B 2 W 41 B 20 W 41 B 2 W 104 B 22 W 20 B 22 W 20 B 22 W 20 B 22 W 20 B 2 W 3 B 12 W 25 B 2 W 40 B 22 W 40 B 2 W 104 B 22 W 20 B 22 W 20 B 22 W 20 B 22 W 20 B 2 W 4 B 10 W 26 B 2 W 40 B 22 W 40 B 2 W 104 B 22 W 20 B 22 W 20 B 22 W 20 B 22 W 20 B 2 W 40 B 2 W 40 B 22 W 40 B 2 W 104 B 22 W 20 B 22 W 20 B 22 W 20 B 22 W 20 B 2 W 40 B 2 W 40 B 22 W 40 B 2 W 104 B 22 W 20 B 22 W 20 B 22 W 20 B 22 W 20 B 2 W 40 B 2 W 40 B 22 W 40 B 2 W 104 B 22 W 20 B 22 W 20 B 22 W 20 B 22 W 20 B 2 W 40 B 2 W 40 B 22 W 40 B 2 W 104 B 22 W 20 B 22 W 20 B 22 W 20 B 22 W 20 B 2 W 40 B 2 W 40 B 22 W 40 B 2 W 104 B 21 W 21 B 21 W 21 B 21 W 21 B 21 W 21 B 2 W 40 B 2 W 40 B 21 W 41 B 2 W 104 B 21 W 21 B 21 W 21 B 21 W 21 B 21 W 21 B 2 W 40 B 2 W 40 B 21 W 41 B 2 W 104 B 20 W 22 B 20 W 22 B 20 W 22 B 20 W 22 B 2 W 40 B 2 W 40 B 20 W 42 B 2 W 104 B 2 W 1 B 16 W 23 B 2 W 1 B 16 W 23 B 2 W 1 B 16 W 23 B 2 W 1 B 16 W 23 B 2 W 40 B 2 W 40 B 2 W 1 B 16 W 43 B 2 W 104 B 2 W 1 B 16 W 23 B 2 W 1 B 16 W 23 B 2 W 1 B 16 W 23 B 2 W 1 B 16 W 23 B 2 W 40 B 2 W 40 B 2 W 1 B 16 W 43 B 2 W 104 B 2 W 3 B 12 W 25 B 2 W 3 B 12 W 25 B 2 W 3 B 12 W 25 B 2 W 3 B 12 W 25 B 2 W 40 B 2 W 40 B 2 W 3 B 12 W 45 B 2 W 84 B 400 W 20 B 400 W 20 B 400 W 20 B 400 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 46 B 10 W 4 B 2 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 45 B 12 W 3 B 2 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 43 B 16 W 1 B 2 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 43 B 16 W 1 B 2 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 42 B 20 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 41 B 21 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 41 B 21 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 22 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 22 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 22 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 22 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 22 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 22 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 22 W 104 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 41 B 20 W 105 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 41 B 20 W 105 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 42 B 18 W 106 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 43 B 16 W 107 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 43 B 16 W 107 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 45 B 12 W 89 B 400 W 20 B 400 W 20 B 400 W 20 B 400 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 166 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 124 B 2 W 3506 B 400 W 20 B 400 W 20 B 400 W 20 B 400 W 17650"));
    }

    @Test
    public void lowerC() {
        Assert.assertEquals("CQ CQ CQ DQ EH DH CQ EQ DQ DQ CH", Solution.getNotes(520, 176,
                "W 17690 B 500 W 20 B 500 W 20 B 500 W 20 B 500 W 10420 B 500 W 20 B 500 W 20 B 500 W 20 B 500 W 10420 B 500 W 20 B 500 W 20 B 500 W 20 B 500 W 238 B 2 W 124 B 2 W 392 B 2 W 124 B 2 W 392 B 2 W 124 B 2 W 392 B 2 W 124 B 2 W 392 B 2 W 124 B 2 W 392 B 2 W 124 B 2 W 392 B 2 W 124 B 2 W 392 B 2 W 124 B 2 W 350 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 266 B 2 W 40 B 2 W 40 B 2 W 82 B 2 W 40 B 2 W 40 B 2 W 90 B 500 W 20 B 500 W 20 B 500 W 20 B 500 W 70 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 26 B 10 W 4 B 2 W 40 B 2 W 40 B 2 W 26 B 10 W 4 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 25 B 12 W 3 B 2 W 40 B 2 W 40 B 2 W 25 B 12 W 3 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 23 B 4 W 8 B 4 W 1 B 2 W 40 B 2 W 40 B 2 W 23 B 16 W 1 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 23 B 2 W 12 B 2 W 1 B 2 W 40 B 2 W 40 B 2 W 23 B 16 W 1 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 22 B 2 W 14 B 4 W 40 B 2 W 40 B 2 W 22 B 20 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 21 B 3 W 14 B 4 W 40 B 2 W 40 B 2 W 21 B 21 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 21 B 2 W 16 B 3 W 40 B 2 W 40 B 2 W 21 B 21 W 40 B 2 W 40 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 40 B 2 W 20 B 3 W 16 B 3 W 40 B 2 W 40 B 2 W 20 B 22 W 40 B 2 W 40 B 2 W 40 B 2 W 48 B 200 W 18 B 282 W 20 B 200 W 18 B 282 W 20 B 200 W 18 B 282 W 20 B 200 W 18 B 282 W 70 B 2 W 40 B 2 W 40 B 2 W 26 B 10 W 4 B 2 W 20 B 2 W 18 B 2 W 26 B 10 W 4 B 2 W 40 B 2 W 20 B 22 W 26 B 10 W 4 B 2 W 26 B 10 W 4 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 25 B 12 W 3 B 2 W 20 B 3 W 16 B 3 W 25 B 12 W 3 B 2 W 40 B 2 W 20 B 22 W 25 B 12 W 3 B 2 W 25 B 12 W 3 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 23 B 16 W 1 B 2 W 21 B 2 W 16 B 2 W 24 B 4 W 8 B 4 W 1 B 2 W 40 B 2 W 21 B 20 W 24 B 16 W 1 B 2 W 23 B 16 W 1 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 23 B 16 W 1 B 2 W 21 B 3 W 14 B 3 W 24 B 2 W 12 B 2 W 1 B 2 W 40 B 2 W 21 B 20 W 24 B 16 W 1 B 2 W 23 B 16 W 1 B 2 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 22 B 20 W 22 B 2 W 14 B 2 W 24 B 2 W 14 B 4 W 40 B 2 W 22 B 18 W 24 B 20 W 22 B 20 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 21 B 21 W 23 B 2 W 12 B 2 W 24 B 3 W 14 B 4 W 40 B 2 W 23 B 16 W 24 B 21 W 21 B 21 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 21 B 21 W 23 B 4 W 8 B 4 W 24 B 2 W 16 B 3 W 40 B 2 W 23 B 16 W 24 B 21 W 21 B 21 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 20 B 22 W 25 B 12 W 25 B 3 W 16 B 3 W 40 B 2 W 25 B 12 W 25 B 22 W 20 B 22 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 20 B 22 W 26 B 10 W 26 B 2 W 18 B 2 W 40 B 2 W 26 B 10 W 26 B 22 W 20 B 22 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 20 B 22 W 62 B 2 W 18 B 2 W 40 B 2 W 62 B 22 W 20 B 22 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 20 B 22 W 62 B 2 W 18 B 2 W 40 B 2 W 62 B 22 W 20 B 22 W 40 B 2 W 98 B 2 W 40 B 2 W 40 B 2 W 20 B 22 W 62 B 2 W 18 B 2 W 40 B 2 W 62 B 22 W 20 B 22 W 40 B 2 W 84 B 10 W 4 B 2 W 26 B 10 W 4 B 2 W 26 B 10 W 4 B 2 W 20 B 22 W 62 B 2 W 18 B 2 W 26 B 10 W 4 B 2 W 62 B 22 W 20 B 22 W 26 B 10 W 4 B 2 W 83 B 12 W 3 B 2 W 25 B 12 W 3 B 2 W 25 B 12 W 3 B 2 W 20 B 22 W 62 B 3 W 16 B 3 W 25 B 12 W 3 B 2 W 62 B 22 W 20 B 22 W 25 B 12 W 3 B 2 W 81 B 16 W 1 B 2 W 23 B 16 W 1 B 2 W 23 B 16 W 1 B 2 W 21 B 20 W 64 B 2 W 16 B 2 W 24 B 16 W 1 B 2 W 63 B 20 W 22 B 20 W 24 B 4 W 8 B 4 W 1 B 2 W 81 B 16 W 1 B 2 W 23 B 16 W 1 B 2 W 23 B 16 W 1 B 2 W 21 B 20 W 64 B 3 W 14 B 3 W 24 B 16 W 1 B 2 W 63 B 20 W 22 B 20 W 24 B 2 W 12 B 2 W 1 B 2 W 80 B 20 W 22 B 20 W 22 B 20 W 22 B 18 W 66 B 2 W 14 B 2 W 24 B 20 W 64 B 18 W 24 B 18 W 24 B 2 W 14 B 4 W 79 B 21 W 21 B 21 W 21 B 21 W 23 B 16 W 68 B 2 W 12 B 2 W 24 B 21 W 65 B 16 W 26 B 16 W 24 B 3 W 14 B 4 W 79 B 21 W 21 B 21 W 21 B 21 W 23 B 16 W 68 B 4 W 8 B 4 W 24 B 21 W 65 B 16 W 26 B 16 W 24 B 2 W 16 B 3 W 78 B 22 W 20 B 22 W 20 B 22 W 25 B 12 W 72 B 12 W 25 B 22 W 67 B 12 W 30 B 12 W 25 B 3 W 16 B 3 W 71 B 36 W 6 B 36 W 6 B 36 W 19 B 10 W 74 B 10 W 19 B 36 W 61 B 10 W 32 B 10 W 19 B 9 W 18 B 9 W 64 B 36 W 6 B 36 W 6 B 36 W 132 B 36 W 132 B 9 W 18 B 9 W 64 B 36 W 6 B 36 W 6 B 36 W 132 B 36 W 132 B 9 W 18 B 9 W 64 B 36 W 6 B 36 W 6 B 36 W 132 B 36 W 132 B 9 W 18 B 9 W 71 B 22 W 20 B 22 W 20 B 22 W 146 B 22 W 146 B 2 W 18 B 2 W 78 B 22 W 20 B 22 W 20 B 22 W 146 B 22 W 146 B 3 W 16 B 3 W 79 B 20 W 22 B 20 W 22 B 20 W 148 B 20 W 148 B 2 W 16 B 2 W 80 B 20 W 22 B 20 W 22 B 20 W 148 B 20 W 148 B 3 W 14 B 3 W 81 B 18 W 24 B 18 W 24 B 18 W 150 B 18 W 150 B 2 W 14 B 2 W 83 B 16 W 26 B 16 W 26 B 16 W 152 B 16 W 152 B 2 W 12 B 2 W 84 B 16 W 26 B 16 W 26 B 16 W 152 B 16 W 152 B 4 W 8 B 4 W 86 B 12 W 30 B 12 W 30 B 12 W 156 B 12 W 156 B 12 W 89 B 10 W 32 B 10 W 32 B 10 W 158 B 10 W 158 B 10 W 4724"));
    }

    @Test
    public void only1PixelWide() {
        Assert.assertEquals("BQ CH DH EH FQ GQ GQ BQ DH BQ BQ CH DH EH FQ GQ GQ BQ DH BQ", Solution.getNotes(420, 78,
                "W 4742 B 6 W 167 B 6 W 240 B 8 W 165 B 8 W 238 B 10 W 163 B 10 W 237 B 10 W 163 B 10 W 220 B 4 W 13 B 10 W 146 B 4 W 13 B 10 W 218 B 8 W 11 B 10 W 144 B 8 W 11 B 10 W 218 B 8 W 11 B 10 W 144 B 8 W 11 B 10 W 217 B 10 W 10 B 9 W 144 B 10 W 10 B 9 W 128 B 400 W 92 B 6 W 12 B 10 W 10 B 1 W 134 B 6 W 12 B 10 W 10 B 1 W 207 B 1 W 6 B 1 W 11 B 10 W 10 B 1 W 133 B 1 W 6 B 1 W 11 B 10 W 10 B 1 W 206 B 2 W 6 B 2 W 10 B 9 W 11 B 1 W 132 B 2 W 6 B 2 W 10 B 9 W 11 B 1 W 206 B 1 W 8 B 1 W 10 B 9 W 11 B 1 W 81 B 1 W 50 B 1 W 8 B 1 W 10 B 9 W 11 B 1 W 61 B 1 W 127 B 4 W 13 B 1 W 8 B 1 W 10 B 1 W 2 B 4 W 13 B 1 W 44 B 4 W 33 B 1 W 33 B 4 W 13 B 1 W 8 B 1 W 10 B 1 W 2 B 4 W 13 B 1 W 44 B 4 W 13 B 1 W 125 B 2 W 4 B 2 W 11 B 1 W 8 B 1 W 10 B 1 W 19 B 1 W 42 B 2 W 4 B 2 W 31 B 1 W 31 B 2 W 4 B 2 W 11 B 1 W 8 B 1 W 10 B 1 W 19 B 1 W 42 B 2 W 4 B 2 W 11 B 1 W 125 B 1 W 6 B 1 W 11 B 2 W 6 B 2 W 10 B 1 W 19 B 1 W 42 B 1 W 6 B 1 W 31 B 1 W 31 B 1 W 6 B 1 W 11 B 2 W 6 B 2 W 10 B 1 W 19 B 1 W 42 B 1 W 6 B 1 W 11 B 1 W 124 B 1 W 8 B 1 W 10 B 2 W 6 B 1 W 11 B 1 W 19 B 1 W 41 B 1 W 8 B 1 W 30 B 1 W 30 B 1 W 8 B 1 W 10 B 2 W 6 B 1 W 11 B 1 W 19 B 1 W 41 B 1 W 8 B 1 W 10 B 1 W 74 B 51 W 8 B 94 W 8 B 63 W 8 B 94 W 8 B 66 W 52 B 6 W 12 B 1 W 8 B 1 W 10 B 1 W 19 B 1 W 19 B 1 W 41 B 1 W 8 B 1 W 30 B 1 W 12 B 6 W 12 B 1 W 8 B 1 W 10 B 1 W 19 B 1 W 19 B 1 W 41 B 1 W 8 B 1 W 10 B 1 W 105 B 1 W 6 B 1 W 11 B 1 W 8 B 1 W 10 B 1 W 19 B 1 W 61 B 1 W 8 B 1 W 30 B 1 W 11 B 1 W 6 B 1 W 11 B 1 W 8 B 1 W 10 B 1 W 19 B 1 W 61 B 1 W 8 B 1 W 10 B 1 W 104 B 2 W 6 B 2 W 10 B 2 W 6 B 1 W 11 B 1 W 19 B 1 W 61 B 2 W 6 B 1 W 31 B 1 W 10 B 2 W 6 B 2 W 10 B 2 W 6 B 1 W 11 B 1 W 19 B 1 W 61 B 2 W 6 B 1 W 11 B 1 W 104 B 1 W 8 B 1 W 10 B 3 W 4 B 2 W 11 B 1 W 19 B 1 W 48 B 1 W 12 B 3 W 4 B 2 W 31 B 1 W 10 B 1 W 8 B 1 W 10 B 3 W 4 B 2 W 11 B 1 W 19 B 1 W 48 B 1 W 12 B 3 W 4 B 2 W 11 B 1 W 87 B 4 W 13 B 1 W 8 B 1 W 10 B 1 W 2 B 4 W 13 B 1 W 19 B 1 W 48 B 1 W 4 B 4 W 4 B 1 W 2 B 4 W 7 B 4 W 16 B 4 W 2 B 1 W 10 B 1 W 8 B 1 W 10 B 1 W 2 B 4 W 13 B 1 W 19 B 1 W 48 B 1 W 4 B 4 W 4 B 1 W 2 B 4 W 7 B 4 W 2 B 1 W 85 B 8 W 11 B 1 W 8 B 1 W 10 B 1 W 19 B 1 W 19 B 1 W 48 B 1 W 2 B 8 W 2 B 1 W 11 B 8 W 12 B 9 W 10 B 1 W 8 B 1 W 10 B 1 W 19 B 1 W 19 B 1 W 48 B 1 W 2 B 8 W 2 B 1 W 11 B 9 W 85 B 8 W 11 B 2 W 6 B 2 W 10 B 1 W 19 B 1 W 68 B 1 W 2 B 8 W 2 B 1 W 11 B 8 W 12 B 9 W 10 B 2 W 6 B 2 W 10 B 1 W 19 B 1 W 68 B 1 W 2 B 8 W 2 B 1 W 11 B 9 W 84 B 10 W 10 B 2 W 6 B 1 W 11 B 1 W 19 B 1 W 68 B 1 W 1 B 10 W 1 B 1 W 10 B 10 W 10 B 10 W 10 B 2 W 6 B 1 W 11 B 1 W 19 B 1 W 68 B 1 W 1 B 10 W 1 B 1 W 10 B 10 W 74 B 400 W 30 B 10 W 10 B 1 W 19 B 1 W 19 B 1 W 68 B 1 W 1 B 10 W 1 B 1 W 10 B 10 W 10 B 10 W 10 B 1 W 19 B 1 W 19 B 1 W 68 B 1 W 1 B 10 W 1 B 1 W 10 B 10 W 84 B 10 W 10 B 1 W 19 B 1 W 88 B 1 W 1 B 10 W 1 B 1 W 10 B 10 W 10 B 10 W 10 B 1 W 19 B 1 W 88 B 1 W 1 B 10 W 1 B 1 W 10 B 10 W 84 B 9 W 11 B 1 W 19 B 1 W 88 B 1 W 1 B 9 W 2 B 1 W 10 B 9 W 12 B 8 W 11 B 1 W 19 B 1 W 88 B 1 W 1 B 9 W 2 B 1 W 11 B 8 W 85 B 9 W 11 B 1 W 19 B 1 W 88 B 1 W 1 B 9 W 2 B 1 W 10 B 9 W 12 B 8 W 11 B 1 W 19 B 1 W 88 B 1 W 1 B 9 W 2 B 1 W 11 B 8 W 85 B 1 W 2 B 4 W 13 B 1 W 19 B 1 W 82 B 4 W 2 B 1 W 1 B 1 W 2 B 4 W 4 B 1 W 10 B 1 W 2 B 4 W 16 B 4 W 13 B 1 W 19 B 1 W 82 B 4 W 2 B 1 W 1 B 1 W 2 B 4 W 4 B 1 W 13 B 4 W 87 B 1 W 19 B 1 W 19 B 1 W 80 B 9 W 1 B 1 W 10 B 1 W 10 B 1 W 39 B 1 W 19 B 1 W 80 B 9 W 1 B 1 W 10 B 1 W 104 B 1 W 19 B 1 W 100 B 9 W 1 B 1 W 21 B 1 W 39 B 1 W 100 B 9 W 1 B 1 W 115 B 1 W 19 B 1 W 99 B 10 W 1 B 1 W 21 B 1 W 39 B 1 W 99 B 10 W 1 B 1 W 105 B 400 W 30 B 1 W 19 B 1 W 99 B 10 W 1 B 1 W 21 B 1 W 39 B 1 W 99 B 10 W 1 B 1 W 115 B 1 W 119 B 10 W 1 B 1 W 21 B 1 W 139 B 10 W 1 B 1 W 115 B 1 W 120 B 8 W 2 B 1 W 21 B 1 W 140 B 8 W 2 B 1 W 115 B 1 W 120 B 8 W 2 B 1 W 21 B 1 W 140 B 8 W 2 B 1 W 115 B 1 W 122 B 4 W 4 B 1 W 21 B 1 W 142 B 4 W 4 B 1 W 115 B 1 W 130 B 1 W 21 B 1 W 150 B 1 W 945 B 400 W 92504"));
    }
}
