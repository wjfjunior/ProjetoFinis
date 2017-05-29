IF object_id(N'[dbo].[FK_dbo.Sessoes_dbo.Sessoes_subsessaoId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessoes] DROP CONSTRAINT [FK_dbo.Sessoes_dbo.Sessoes_subsessaoId]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_subsessaoId' AND object_id = object_id(N'[dbo].[Sessoes]', N'U'))
    DROP INDEX [IX_subsessaoId] ON [dbo].[Sessoes]
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Sessoes')
AND col_name(parent_object_id, parent_column_id) = 'subsessaoId';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Sessoes] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Sessoes] DROP COLUMN [subsessaoId]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201705290242389_ModificarSessoes', N'Finis.DAL.Contexto',  0x1F8B0800000000000400ED1D5D6FE3B8F1BD40FF83E0A7BB622FDE6451E0BA48EE90739283D1CD07D6D945DF168ACD78D5CA924F9283A4457F591FFA93FA174A49A4342487142959B6B3272CB0884971389C2F92430EE77FFFF9EFE9CFCFABD07B22491AC4D1D9E8F8E8EDC823D13C5E04D1F26CB4C91E7FF871F4F34F7FFCC3E9E562F5EC7DE6DFBDCBBFA32DA3F46CF435CBD6EFC7E374FE95ACFCF46815CC93388D1FB3A379BC1AFB8B787CF2F6ED5FC6C7C76342418C282CCF3BFDB889B260458A1FF4E7248EE6649D6DFCF03A5E903065E5B4665640F56EFC1549D7FE9C9C8DAE8228488F2ECE3F8CBCF330F0290233123E8E3C3F8AE2CCCF287AEF3FA564962571B49CAD69811FDEBFAC09FDEED10F53C2D07E5F7F6E3B82B727F908C675430E6ABE49B378E508F0F81D23C9586EDE8AB0A38A6494689794B8D94B3EEA827067A3F34D1627234FEEE9FD244CF2AF38514BE21F155FBFF18AB23715CFA968E4FFDE78934D986D127216914D96F8E11BEF6EF31006F3BF9297FBF81F243A8B366108D1A108D13AA18016DD25F19A24D9CB47F2C8900C16236F2CB61BCB0DAB66A04D39826994BD3B197937B473FF212415B7C168677458E4571291C4CFC8E2CECF329250664D17A4A097D2BBD4177926AB75E8275F944ECDEDA2784578032A9654B146DEB5FFFC8144CBECEBD9E8CF5493AE8267B2E0050CF14F5140D590B6C9920D4106D680ECCA0FC2EEBD9A3BC948481EE3C8343AFAE7163A9A93704329DF7B3F245A9084CCE3A91B873729A15211D1FFB3DE512CFADAAC1754847BEF2BEF441AD7052DBA0F56764D4534B54D6FFCA76059A8A8861D23EF23098B0FD2AFC1BA34F8A595FA527F7295C4AB8F71C8AD5D55F365166F92798E458C56DFFBC99264F6485D32338022C52BBF149D9014A225D7553D73C4940F38E610B5D3716DDD8D36FFB2A28CA5D9E70D06CB2FF715C6CB84CEB79B2436A8DCBB6D58D468B322752FBA913518CB984A1159D1A1F58EEE831F2446A29C6CA317BA347432C7F360E12FC860C30FC28697CCC08D2537C1FC1B602BC52AC586CBF59811B7B69413D6BDA59D2C3F1FACA4E33AD7D2E0B8AE73D38C5A6645D71DA10CCADFD302AEE00EAAFCA5167DE15FD4AA2F54288A2FD67652FB4BD6B5EDF2A8F87C50FB8350FB345886BE69DDD147A76B3F48074B73989626E70DBEC8282D45590F161875B1BAB800759D2CCC5DD1A9A57DC93F1EACCBEFD7BA0C96A1BD65B09FF32BE78DEDACCF1A0C9AA9387F836C136EC1C3E0ECDDC845E9C99FFB75DFC57A12963BC20CD287A8DBB4EE47B12AA1AE5B99450006D50E8F75BE299EC4AB7552D9AF0B320F567E38F2EE685DC08EF97E1C79B3B99F836DD9C767122DFAEB6241D27902A981BA96B6E15B5A9334EE6D184F39916EA330A88F487E89E912C38F9C41FDB6F1A956967E8A2E0242858CAABDDF75191B2C026A3CBB4249499AFAC3E65DA7047B5E525747185DCF3994F5B5EE20C4DAAD500AB119B3EA23043356A7C78C7FE08A59A91766C4F837085E65951E2D56EF8A55A96766ACF837085665951E2B56DFCD19C3B965BB2E2BBFEFB82CF37EF153021611577112913959E49725C6467CA78C8996E8969F0F8B48C7ED9DE5F1D1E0C7F966261DAD1FA7D420C58F038A150305EB3A19A719B38D96CA5E7E3E28FB4128FBA0A83B70AB9C3FF961506EBD6DEF18F216839A0C12BB07899D8401E586C3A17FF9FD20ADCA1EDE0F17F12429364C777E320FFCEADAE9D6FD29B0AFCB744D7AED8C0A9B7FE3A7142ABC49D5DABBB7CC292CBA2CEF8375FC2B2B7684962C773E910ED798876BCCC3C4B3F56BCC6C62412F32CB75EA6D18F9834EDB1CE8FDB09C16EB26C3CCA8687BB4FE7BDFA66B30CA83511E8CF2D68D726DD650BB8C542BA619FBA69375BE4FFC2875DA63572D06DBAC9CCCFA619CF4BA75E8BC61C8E8EE00F05CDA37809AC12176100665CE3D0A883DA9B8F5A5FAAAB6264AA5624BD42FDC2CC966053D1FF0A2CC34BD0AFD651D1B6DEB0DA961743D81FB941BC7F085CA04D47D91E6D764F5401286FFCDEDE7DB91F7D90F37F4C7B1C21FE1DB4FB3F38BFAE313955E25650CD4827BF496C4AA41EC9C565797D7D39BE98D35BDAECF67934F1F6083B63403F6A903D9B63583B953EEF2E6FEE3F9C5B92DE166E753F0B115D1CED3349E070505848084F2884BECEB325A78867BCBF5D4C0631AAE29558235A503B50767A33F29C8E300AB80D41A6079855904773C921700B7D1055DAE67C43B9F970F204CFC745EDC55921701B463B184AE19E8E228CA9F69C8AD0AE5611065EA022388E6C1DA0FF5484B4D2C5725394A1570B9E682ACF3A55B94E9696FD32B3F6E567BAE3A9088D44493D331101EB34C8961363A21D0C4DC80DB95ECEA99BD5CE1813A8DA2BA7FD94211DF8174A13CB0E9B70E81DB8B84C9519C5A43A30BE9042251EDB51CEC972612B45178DF1E1DA946BD9D35C251D88545C2696AD3731D24BD17A9915E6FD07157F79443CD5CF6B48DBDC0689E7FB010C3ED490C8EC30E0406A7A7959501EEABFDD819F9CAA9D62268EF9F0216577120162C368046A4C75220DB591ACDD076616A3443B7921DF872D27E8587DF0A6EE4B07245B859788CD394EE663184CBEFC01EDA924887FC2EC54EE28795D8D5B10E7B113A7865512717E8FDC55A24F835637B41C32E3D1EFC7E0E417A07B285D0FEE0F773721441A3BD91430AB664C6A448844691DDBF8C6950DFA50D137961D7338FB3DAAFB4B1E89046B1904345B6246D52840900CBAF661FACB489A8EF52DA445ED8F45CC7E3EDC757255F85D1FA96B4F762C0A69F1FB438B8AC74B76976BA37D461B10B179486AEAF627F889DD9EB386D3CC0C7C3C1ECC5C874F2BF53493220B2036132D0F855C8937A66AB63B8E100171CDAD70762F6A2A43FF86DB674AA1C69075F9E1B538265945CD59956FE9B3C67D87D934F29618776293B9C974790039C910C7AFB465E7D3E2DFA4A94F18BAD6B4D5100D4550D30B80F5881C02B9A7060A7152A06ACA2A17DB9EF525A97C54D7D574B27B5F7AAAA01065F952B107845437BBECE52DAF38A86F62042489582BAAE898B5CD45536F29A0608D0A42B4074E1BF081CA0CE0A1850274101FA278B168B6C045F200F58C976D07C2E5CE15C8BAF6249CD07C100022AA96371401683959EFD53876B38B014176BE891254018576C030C64D01AE5761FB6F2D429C267D3399AC829CD491A445C63178D70100268ACA33B01E4D7BAD5F19B4E8404B4356742006B745A31C1C058AF9B595A305F792C03E1BEF174C3EE7C0362AF9917EC4E339A48D98506D5B31C061AA04E7A3B377D6B1AC87E7900C8302DB8134288635769A0F519377A8D01C2F89CDEE826DEBEB557DE3C31301D73685AB9345BB35CF2613612B00B01F8F32A0602603E362B2F5B6B02486E350047B3AA6B31DF2BE14DC8946FF4FBD8797EE09C852F05ED5C3DFDCC00683C814A88461786B513C3CA7A597B2DFA210A72295A2549C32EDC721F0E06A05D9C5B6EBC9B05CD400A7E45BBDA6A5775A7E332D3152B381D6B52629D5EFBEB75102D418A2C56E2CDCAFC58931F66EE19A456258CF15C987564C740D5533E532E89549B7B0C16E42A48D2ECC2CFFC073FBFEC3B59AC90CF986341B3C5E2DD40DF81CA2ABEE9E25FE77F336DE319C28ED0B635C9AEE828F2E8EE62404459F528EDBC3C1F994F2D2712B93289C3CD2AD27BCEF4AD85DB11108CF1DA841E5E1913080195250E1895017E022E65913D8C3A7E0F82A94BED2155017A105055E8302AE051148666F034EAA1096134109C50E1088FC7A028F078853D3C211C06C2132A1CE161F809152ABCD3B1A46C8A2F575165E5404EB40B5656E3523F7FDA180E5D730BDBA16FDA8FF980C989201458EE603C580222C17CB032079585298804B58515F6F0789E21088A97B91892B56C44D64EEDAB2BB30210ED455A3DA4C1781CB4F198E83C7036A6036F6C6138740DFB311B5B5825547107C26CAA8D46D0431AD4E1A0D5E152E78BB79A49D1C636F3A8A6E1A1AA034B1A0141B0227B18FC6A2104A2BB6EA88732A8D341ABD31DEEDBB55126ACA9852AE1CDBE65451A54E0A055A07253B79C5334CD6D66156DD37ED4816749119C20ACCC653F05DE6C10F753A0C21E5E99F044185551620FA14878020114050E8B4796EC44583AB23287F912A63B11264D58E1088FA53651C0B17207A5ACB398082A59173B6056E42911702A4AEC2108C9482020A1C21E1ECC4802C1C1722779E0B13A9248E84278F4B0EA4BF3A2DEEAAED2EB21D517A285D9487B4D5A0F6998900E7A420287642DA724FCCA82C58CA4EF5A4F5DEB3969124714B1FC522B7B9AA48A3184CD2F82DC22AD82C847EFCD74E74A0319A769FEF7EDE377353D6BAA7C7F6834C567E8E2B147616A2E4A86639BE1D8E6F76887AD0DA749550FCC76C29B143B359F2D27B5A9EE46950D5FF0C6163CD1353C5477C3E0737B4D76A5A52ACC7477EB6C54016F6CA10ABA8687AA0A83101FB410D77132EDE458DBDE42940D6DFB91E641160F5A1627DADB985667E4786B0B39D4B6EC470AD1EC36822F08FBA01DFC3AA38DAE83FA0B27B912D2D848A225D4D943E5D96C20345E660F25CF620321E4BF870DF3B0611EECB0B51DAEAFB3B7B3C4DAF616B6D8D0B61F6BCC5EED170E2FCA22377BA85A41B7233DE1796BF1644FA81AB4ECB5699918B9A151B53A5EC5A851F567D68A9347A04894D246A2A8B4B1D2380E057FDD03F4DF0635EDFB1EAD8D8111A5DA43374DF32C18D513F37603960375548950E275E44F2A796425D5EF2A5E87C5CA08413C0541F2909C8210298BDB918367CA4FF2AC24F153B0C80367662F69465647F90747B3DFC2722D5C7F70ED47C12349B3F2E1FBD1C9DBE393A3F37F6E923CB08A6EDED232A48A0507BD971FF5B08A163A7E97470B91C56A2C37778F39CAA1A4E942C82F83A49655437176918226C809DB9864C63571160CE801FD28EFAB4CE952EDF96CF4AFA2DD7B6FFAB72FA0E91BEF36A1DC7EEFBDF5FE0D5170CD17163DD1FDCAD7E245363177579774601640DB64FFE260BF5BF9CFDFBB0293327C75820517D18E1CAC5A76612092BDA7D378900C3D9DE0215978F2A24CCA80640D4A44CD0E94754A2D3C54E7D59A1718F163D0C677CEDAC8038000EE8EA809713F5BC58D870119809E38032D0283D0D15AB5AE2282DC2C046F37D88703B10F5844CEABB50E8D53FFBB16537F15ECE33817B2763A496F9BC86E10F5D653219620681075252AC134CD38C3E4D70CDC94A76C35A8CEA1A88E1A5D33284ECF8A33C8EF364D3F1A12F36A659847D76C578A85409BF6FBA032CCA67DFB22CA462F05768B361666D31E0B21BA8661B3ED1CCA30E4A6A72E40208E71F7E8BC7D2C43737AC25A08D729FB7808DC7908C374DA4B0288CF71DC01F0869DD631754C8F5BEFBC5DA7CEEB3020B7CE79BB6105772833A0EE1AF9AB9D03CBD893AD7AE4879383E1E4E09BB7182A2829B6C320AAC7273F362880B53DC242285EAD2D6A341C27EE8663709C1CB012598B39161E3188F9206DFD489B2682E1D50ADC201CDB3CE943533CBD56D14083127A7247E0F1097D796CA480858E9E301EB1D0DEFF91472C6CD7E60FFBAC619F35D8656E973537A55FAD6566E10A3D9AC7CE46518A61686F1B074568030A5E5777BC3E56B5B4375F3A45445241EBD2D081AC1965D6A012EBDD649B97D3D7891956595153BECD56C955354F71DA5821CF299DB3EEFD5CB5A7BD26A637E4ED6BC3A59EA464229C780909407B911207EE75CDDDAC7B765CED49FF44F76E92CA9B121DB6E1159A31594D7368957D794B7C77E04657FBA0CF54A1F6A5CF55B013CE9B523CB6655803F359FE42008895F4A3EE2ECCE8C8785D6E2344DFF79D4ADB9CDC12C94C2772BE2A3C64CEEB9F24DE27E74D19A476CB7A34A7279A7F4EE0192CDECD3AD24504B7243CC637F0B63F63B888AAE101DE9DC88F361FEA816C3B78A650D03B2F7AD5DB0EEDF37F07B6ED30A68F6DC3A76FC8AC38F070972645FF10F76E2506CBB70B38C653E0027EF1A26F5662B42F1DEE5762F40FAEEFC6B561CC4FDCD3C66502DF181113257F039B17FD037007B87D694CCBDC9308382F895FA120382E7DF72E0B0DF9A8DB2970831C28A7316266ECBE5C580E2ADA51084C8F90214E2CE30B465B9701E905203E2728A9AE65EEB1D79EA0EB8B670F577D12E55B3F67A3C543CEE4F2B4863B4B54C11041D7D645815E57611DF0DAE62EB8DF55E9805760E0CB3A0BFCD901808A3DAB40712FEA9A81975B410574598C01CE6B6C90AE16622ADA55158A38ABB5E8031A7AA5175889F553D75BF4C4B7424A2FBC02EBA1AC6B06CE57CD0A705E8101CFEB620BCCEB5B7D886ED575A87E95D536BD54765B5580EA8D374C03CACA66F8C0962B3D803AAC0F568D8D427CDD0C39C7F5C01782CE21FE16C4E3528D821728D6583DF8036D7891BC2712D1B6189278F4880CCA7036D90E4971A2178C234CE5DB7D68F25919C631D3715A3B44352B5A4896AAB0F310A543216484A663A3F6A80A2D85E9B97CF07C8324F368C341F9EC0363A1F17C44E3158043AC0AF73B44EEE3370D113D07D06E7B00B6B0D824AE2E046A3158E89046C6A9F55777B49FE20C0D13AD6C917FCC876A621FE6666D87E84EB9263BFD4C43C4FC82A86710A0CA8BF63844C54B854D84464FD6966CA9B458129EEDEF3E4CCC19838CB4D167B3A5C13A1BAB1643465E145607DCE09868CB219D2302B4D43E786D18AAF2B06E55773A2E57B9AC80FE541ED0A59BF94D94DFAD2C7F5D903458D620F2578129ED856D7CF5CD347A8CB94741C2887F22DDC7BC26740148F7F8E749163CFAF38C56CFA9B207D17254E5545C3D90C534BADD64EB4D46874C560FE10B2446EE9530F57F3A56703EBD5DE7BFD26D0C81A219E4D7516FA35F3641B8A8F0BE4242543520727707BB069DF332CBAF432F5F2A48374AAA031D2046BECA4B735FD83FBA47BA8D66FE136983DBA7947C204B7FFE72C7DE41D60369668448F6D38BC05F26FE2A6530EAF6F42795E1C5EAF9A7FF036240901545F90000 , N'6.1.3-40302')

