
public enum Card_Mark
{
    Spade,     //  黑桃
    Heart,     //  红桃
    Club,      //  梅花
    Diamond,   //  方块
    COUNT      //  JOKER 无花色
}

public enum Card_Point
{
    _A,
    _2,
    _3,
    _4,
    _5,
    _6,
    _7,
    _8,
    _9,
    _X,
    _J,
    _Q,
    _K,
    _JOKER1,
    _JOKER2,
    COUNT
}

public enum Game_Loc
{
    Deck,
    MyHand,
    OpHand,
    Neutral,
    MydianshuZone,
    MylingkeZone,
    OpdianshuZone,
    OplingkeZone,
    dianshuGrave,
    lingkeGrave,
    Unknow
}

public enum Zone_Seq
{
    _1,
    _2,
    _3,
    _4,
    COUNT
}

public enum Card_Pos
{
    FaceUpAttack = 0x1,
    FaceDownAttack = 0x2,
    Attack = 0x3,
    FaceUpDefence = 0x4,
    FaceUp = 0x5,
    FaceDownDefence = 0x8,
    FaceDown = 0xA,
    Defence = 0xC,
    Stand = 0x10,
    COUNT
}

public enum Phase
{
    PHASE_STANDBY,         //   准备阶段
    PHASE_DRAW,            //   抽卡阶段
    PHASE_MAIN,            //   行动阶段
    PHASE_DISCARD,         //   弃牌阶段
    PHASE_END,	            //  结束阶段
    COUNT
}