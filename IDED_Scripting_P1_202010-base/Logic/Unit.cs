namespace IDED_Scripting_P1_202010_base.Logic
{
    public class Unit
    {
        //limitar rango entre (0-255)
        #region Base 
        public int BaseAtk
        {
            get => baseAtk;
            //protected set => baseAtk = value;
            protected set
            {
                if (value > 0 && value < 255) baseAtk = value;
                else baseAtk = 0;
            }

        }
        public int BaseDef
        {
            get => baseDef;
            //protected set => baseDef = value; 
            protected set
            {
                if (value > 0 && value <= 255) baseDef = value;
                else baseDef = 0;
            }
        }
        public int BaseSpd
        {
            get => baseSpd;
            //protected set => baseSpd = value;
            protected set
            {
                if (value > 0 && value <= 255) baseSpd = value;
                else baseSpd = 0;
            }
        }
        #endregion
        public int MoveRange
        {
            get => moveRange;
            //protected set => moveRange = value; 
            protected set
            {
                if (value > 0 && value <= 10) moveRange = value;
                else moveRange = 0;
            }
        }
        public int AtkRange 
        { 
            get => atkRange; 
            //protected set => atkRange = value; 
            protected set
            {
                if (UnitClass == EUnitClass.Ranger || UnitClass == EUnitClass.Mage)
                {
                    if (value > 0 && value <= 3) atkRange = value;
                    //else atkRange = 0;
                    else goto Retorna;
                }
                else if (UnitClass == EUnitClass.Dragon)
                {
                    if (value > 0 && value <= 5) atkRange = value;
                    else goto Retorna;

                }
                else atkRange = 0;
                //atkRange = value;
                Retorna:
                atkRange = 0;
            }
        }

        public float BaseAtkAdd { get; protected set; }
        public float BaseDefAdd { get; protected set; }
        public float BaseSpdAdd { get; protected set; }

        public float Attack { get; }
        public float Defense { get; }
        public float Speed { get; }

        protected Position CurrentPosition;
        private int baseAtk;
        private int baseDef;
        private int baseSpd;
        private int moveRange;
        private int atkRange;

        public EUnitClass UnitClass { get; protected set; }

        public Unit(EUnitClass _unitClass, int _atk, int _def, int _spd, int _moveRange)
        {
            UnitClass = _unitClass;
            BaseAtk = _atk;
            BaseDef = _def;
            BaseSpd = _spd;
            MoveRange = _moveRange;

            if(UnitClass == EUnitClass.Villager)
            {
                BaseAtk = 0;
                BaseDef = 0;
                BaseSpd = _spd;
                MoveRange = _moveRange;
            }
            else
            {
                BaseAtk = _atk;
                BaseDef = _def;
                BaseSpd = _spd;
                MoveRange = _moveRange;
            }
        }

        public virtual bool Interact(Unit otherUnit)
        {
            if(UnitClass == EUnitClass.Villager)
            {
                goto noInter;
            }
            //else if (UnitClass == EUnitClass.Squire)
            


            noInter:
            return false;
        }

        public virtual bool Interact(Prop prop) => false;

        public bool Move(Position targetPosition) => false;
    }
}