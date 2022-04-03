using System;
using Xunit;
using Codingame;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string[] testData = new string[] {
                "ZZZZZZZZZZ",
                "ZAAAAAAAAZ",
                "ZAAAAAAAAZ",
                "ZAAAAAAAAZ",
                "ZAAAAAAAAZ",
                "ZAAAAAAAAZ",
                "ZAAAAAAAAZ",
                "ZAAAAAAAAZ",
                "ZAAAAAAAAZ",
                "ZZZZZZZZZZ"
            };
            var volume = Problem.Solve(testData);
            Assert.Equal(1600, volume);
        }

        [Fact]
        public void Test2()
        {
            string[] testData = new string[] {
                "ZZZZZZZZZZ",
                "ZRRRRRRRRZ",
                "ZRRRRRRRRZ",
                "ZRRRRRRRRZ",
                "ZFFFFFFFFZ",
                "ZDDDDDDDDZ",
                "ZDDDDDDDDZ",
                "ZDDDDDDDDZ",
                "ZDDDDDDDDZ",
                "ZZZZZZZZZZ"
            };
            var volume = Problem.Solve(testData);
            Assert.Equal(1056, volume);
        }

        [Fact]
        public void Test3()
        {
            string[] testData = new string[] {
                "XXXXXXXXXX",
                "XSSSSRAAAX",
                "XSSSSRAAAX",
                "XSSSSRAAAX",
                "XXXXXCXXXX",
                "XSSSSCBBBX",
                "XSSSSCBBBX",
                "RSSSSCBBBX",
                "XSSSSCBBBX",
                "XXXXXXXXXX"
            };
            var volume = Problem.Solve(testData);
            Assert.Equal(449, volume);
        }

        [Fact]
        public void Test4()
        {
            string[] testData = new string[] {
                "ZDDDDDDDDDDDDDDDDDDDD",
                "DAAAAAAAAAAAABBBCCCC",
                "DAAAAAAAAAAAABBBDDDD",
                "DAAAAAAAAAAAABBBDDDD",
                "DAAAAAAAAAAAABBBEEFF",
                "GGGGGGGGGHHHIIJJEEFF",
                "GGGGGGGGGHHHIIJJEEFF",
                "KKKKKLLLLHHHIIJJMMMM",
                "KKKKKLLLLNNOOPPPMMMM",
                "QQQQRRSSSNNOOPPPTTTT",
                "QQQQRRSSSNNOOUUUTTTT",
                "VVVVRRWWWNNXXUUUYYYY",
                "VVVVRRWWWZZXXAAAYYYY",
                "VVVVBBBBBZZXXAAAYYYY",
                "VVVVBBBBBZZCCCDDEEFR",
                "GGGHHHIIIJJCCCDDEEFR",
                "GGGHHHIIIJJKKKDDEEFR",
                "GGGHHHIIIJJKKKDDEEFR",
                "LLLLLLLLLLLLLMMMMNNR",
                "FFFFFFFFFFFFFFFFFFFF"
            };
            var volume = Problem.Solve(testData);
            Assert.Equal(368, volume);
        }

        [Fact]
        public void Test5()
        {
            string[] testData = new string[] {
                "YENZRGRNFKINMCAGFMMGJWVZZRVCWQ",
                "KKBAQPBNSWUXIDFHDZYNTOGTSAEKVS",
                "EGTLNXYWRHLLYTNGOYBLULNOIGVMFV",
                "WKNZBYVPCWLXZSESPZKIACDBLGNTJZ",
                "MUREWDLLJVTOGPOHWPGIRUDOLEGTRL",
                "LZKAVBAYBMNMOAAQGQKDCHGNOIBNNZ",
                "URHBKIZDYSCPSGEHRIGIFEUYAWLYLX",
                "YASRATWMWKORQIJYHKCYSWRLZIYBYX",
                "YCDYJTUOFHCWTEXEWIOJFUVVFSJLWL",
                "GUAWVRQETAUNHQLMARPFPFXCRRGVOI",
                "TGMCABQJLFBRCYDQMWEKZJBYBFMADN",
                "GZMUVJKJFZSEWAMIIZWPMWLOQRQJJH",
                "JOCNZHLCORENIIJQINMRKZDOKPYJNW",
                "MDGUSXCCKXWQGCFZRBDFGTCHHBFGSB",
                "QYAZGXUEUMZFUDCSGJZUFGHGUUBWIH",
                "NUVZDLUUYJYRGQSYHJJUKIAJGEKUQN",
                "CNBQBDXAFDXNMLSZUOCLOCGYKVOVNE",
                "PARZOFUPFZTGRITQXCMOZKXHCKBRMA",
                "IVOKPXQGEHOMLSWIEATJUXAEDBKUFJ",
                "XVLSZSAOWZOEQCZWBKOTWHNGFQNOQN",
                "ORYFEBJITOHJHJZHCLJNFZRGLHQRJA",
                "KZYENAUZGROOZIPCBKRBUNJWUPJRIG",
                "YNEPCOUEPQFEGGXSTLKWKJJJJORUZB",
                "GFNHOUWLRQVESEOFDMBQQDCACXLGYF",
                "EBNNJKZYYOGGVUIDIEROUTFNNSBBXB",
                "BHRKDBVBBLEMPYVOOKGUPUGAQNMBGT",
                "ZDOLHRCSCLHTIXRPCTZMFQXCRNNMUT",
                "SXBRAATOMKNRFGEMNEZKCSXMPKKMJV",
                "DKMJHUYLWSHBIXJHMXEOSTLMEXUPQB",
                "SZJSOCBQVELVOOWHGJCTSQOYNFTOKP"
            };
            var volume = Problem.Solve(testData);
            Assert.Equal(2440, volume);
        }

        [Fact]
        public void Test6()
        {
            string[] testData = new string[] {
                "XZADHVCOMBACBBHTEYCCXJDNNLMYYDVRQSAOWNOI",
                "RZTKUWUMFVHBNEXKYVLQDRUFYAQKNIAIMJKBTGTA",
                "EHAOTIYXUEVIQOXLMWTXJTXHNEZIBUQDRHGWTJOV",
                "SMNHPIQCWIHIOHQBNMINYYUPKZWHSUFWKUMTENJA",
                "VLFBINPJBIIXNHREZHUCUEIXMDOBMRMYYFIGTYEA",
                "WRYSXJRQCGWOCBGQQORMTHLRYRQHACUUNRCHIRFV",
                "JZBZWXKXMCTICXQALXZKHNXMTGPXXTQMMLHPEHGL",
                "TTSURBLRCWGWSTVVELLVTCLJCRKTFSDBAOLGRHHN",
                "OQLAKHARLJNJDIYHWSJRETNNZOVXEDOGNFJBPSXT",
                "GZSTCXPBKPARDYSQGUUCKCSSPQAIKNVVFIXHWKUK",
                "KJIAHTGEOGFBMTCQMZFCZGGTALBHOIZDOAEAVUKX",
                "NABDPPDWUGXPMLOFHJBZJFNALQLXWNSPTCEOWDGD",
                "ELAECPECRDVSKPUWUECZONMBZONPMZVFJQKCYHLC",
                "XTFRWZHKFTZOCHDBVJZPPPHXYVBQEAWBOGEFXYIL",
                "JIBXPJDKZPLTINBAOBUBZIUSISQPQZKUXZEGFHVV",
                "BGVLWHNHYJNRMTQWWCLDGQVFNSHYDPRESKBWJMBN",
                "SUFTWVUHUNNBXLBLVPHFNUADPBZIYOCMMJTOGBRU",
                "VXUQTYNINPBJMLHPKFPDVCBYZCAKWASDPJEFYQPP",
                "FUOSGBXGUZSODPITUAYOUFGCTYOBQCRCPFQMXFHY",
                "JIHWAPIPQEKHFDULYPVKZVEOHWFZQJVKVIGIQRLI",
                "DPIWHKRZVKYOEINTAVFMPBXLVYMGDHCSZIQPELHM",
                "CJTVHFCMJBOGXWYWAHWXRASNTJFTAXCSJOELOYIN",
                "ARCQUIJKAJIPLKQNFTYQZFMMHADTCBPRQQUVJXOV",
                "OEUOOYJNXOUNFZWOOSOYCDISWAMEVFYZFTDCFDFG",
                "OTQRWRPTUSIKRRYKJVEYGSXQSEGNNPQPVKJJDWDU",
                "RBIIAEUNUCRKWZWNRLCBRVBFIOWWDBXLWTMBGFIW",
                "UWYVUNGLTPOAUIVZTFDNAVGPZWSVOIZGBDXNFWKF",
                "PRNRBRKQTKZPZRPXTJXZCACJFAKTGOVOWRXIQIEY",
                "ZEJHECJGTJYSAVCBXLKUCDVIGQQGRXNSCOFZSRZJ",
                "RVUJLHKDVNTYXCWGKPPSIWWTSOJXUYVAEVIFOXKH",
                "PGLLUAICYQRASURUOGTJNKHAXJLNOGOKXQHQYKRS",
                "NSVKBFGQFQHPCPUENNCPNWEEJUGUMVFUVXXFMQBA",
                "KTXYSCTFRGDRRFTKRQFDQTLFNFWRDOMVTQLRLIOX",
                "BQMCCWRYMHCAMSSRGRPCEPZTYIWAMYQCHSBCAKIM",
                "LTURGHLZQJZSTFXPPVBHZKKNAMTYPHCVEBFKBABY",
                "JGYXBZRDJEYUCGFYYZLZXBLUKFHMAJYIDKKWFKRE",
                "XLLTZANFRHDLEGTCSHWEJYNXNVGXJAJMXPJNVGSB",
                "WJQQOLKPVIYUFFEMFOJHTJCWASTILFGGHWUECWAJ",
                "HIQTTIUXRSIKZIBXVKMXEKQKBUGFUMEZGZXCBKQE",
                "DEHPKLWEXHGMYRYZTGHUAQVIHUZKRTVTARLKCVBN"
            };
            var volume = Problem.Solve(testData);
            Assert.Equal(4832, volume);
        }
    }
}
