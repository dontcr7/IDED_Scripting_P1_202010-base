using System;




namespace IDED_Scripting_P1_202010_base.Logic
{
    public class Unit
    {
        private int baseAtk;
        public int BaseAtk
        {
            get { return baseAtk; }
            protected set
            {
                if (value >= 0 && value <= 255) baseAtk = value;
                else if (value < 0) baseAtk = 0;
                else if (value > 255) baseAtk = 255;
            }
        }

        private int baseDef;
        public int BaseDef
        {
            get { return baseDef; }
            protected set
            {
                if (value >= 0 && value <= 255) baseDef = value;
                else if (value < 0) baseDef = 0;
                else if (value > 255) baseDef = 255;
            }
        }

        private int baseSpd;
        public int BaseSpd
        {
            get { return baseSpd; }
            protected set
            {
                if (value >= 0 && value <= 255) baseSpd = value;
                else if (value < 0) baseSpd = 0;
                else if (value > 255) baseSpd = 255;
            }
        }


        public float moveRange;
        public float MoveRange
        {
            get { return moveRange; }
            protected set {
                if (value >= 1 && value <= 10) moveRange = value;
                else if (value < 1) moveRange = 1;
                else if (value > 10) moveRange = 10;
            }
        }
        public int AtkRange { get; protected set; }

        public float BaseAtkAdd { get; protected set; }
        public float BaseDefAdd { get; protected set; }
        public float BaseSpdAdd { get; protected set; }

        
        public float Attack { get; protected set; }
        public float Defense { get; set; }
        public float Speed { get; set; }

        protected Position CurrentPosition;
        private EUnitClass unitClass;

        public EUnitClass UnitClass { get => unitClass; protected set => unitClass = value; }


        Random rdn = new Random();
        

        public Unit(EUnitClass _unitClass, int _atk, int _def, int _spd, int _moveRange)
        {
            int rdnx = rdn.Next(0,100);
            int rdny = rdn.Next(0, 100);

            UnitClass = _unitClass;
            BaseAtk = _atk;
            BaseDef = _def;
            BaseSpd = _spd;
            MoveRange = _moveRange;

            CurrentPosition = new Position(rdnx, rdny);

            switch (_unitClass)
            {
                case EUnitClass.Villager:
                    BaseAtkAdd = _atk * 0;
                    BaseDefAdd = _def * 0;
                    BaseSpdAdd = _spd * 0;
                    AtkRange = 0;
                    break;
                case EUnitClass.Squire:
                    BaseAtkAdd = _atk * 0.02f;
                    BaseDefAdd = _def * 0.01f;
                    BaseSpdAdd = _spd * 0;
                    AtkRange = 1;
                    break;
                case EUnitClass.Soldier:
                    BaseAtkAdd = _atk * 0.03f;
                    BaseDefAdd = _def * 0.02f;
                    BaseSpdAdd = _spd * 0.01f;
                    AtkRange = 1;
                    break;
                case EUnitClass.Ranger:
                    BaseAtkAdd = _atk * 0.01f;
                    BaseDefAdd = _def * 0;
                    BaseSpdAdd = _spd * 0.03f;
                    AtkRange = 3;
                    break;
                case EUnitClass.Mage:
                    BaseAtkAdd = _atk * 0.03f;
                    BaseDefAdd = _def * 0.01f;
                    BaseSpdAdd = _spd * -0.01f;
                    AtkRange = 3;
                    break;
                case EUnitClass.Imp:
                    BaseAtkAdd = _atk * 0.01f;
                    BaseDefAdd = _def * 0;
                    BaseSpdAdd = _spd * 0;
                    AtkRange = 1;
                    break;
                case EUnitClass.Orc:
                    BaseAtkAdd = _atk * 0.04f;
                    BaseDefAdd = _def * 0.02f;
                    BaseSpdAdd = _spd * -0.02f;
                    AtkRange = 1;
                    break;
                case EUnitClass.Dragon:
                    BaseAtkAdd = _atk * 0.05f;
                    BaseDefAdd = _def * 0.03f;
                    BaseSpdAdd = _spd * 0.02f;
                    AtkRange = 5;
                    break;
                default:
                    break;
            }
            Attack = BaseAtk + BaseAtkAdd;
            Defense = BaseDef + BaseDefAdd;
            Speed = BaseSpd + BaseSpdAdd;
            if (Attack < 0) Attack = 0;
            else if (Attack > 255) Attack = 255;
            if (Defense < 0) Defense = 0;
            else if (Defense > 255) Defense = 255;
            if (Speed < 0) Speed = 0;
            else if (Speed > 255) Speed = 255;
            
        }

        public virtual bool Interact(Unit otherUnit)
        {

            switch (UnitClass)
            {
                case EUnitClass.Villager:
                    return false;
                case EUnitClass.Squire:
                    if(otherUnit.UnitClass is EUnitClass.Villager)
                    {
                        return false;
                    }
                    else
                    {
                        
                        return true;
                    }
                    
                case EUnitClass.Soldier:
                    if (otherUnit.UnitClass is EUnitClass.Villager)
                    {
                        return false;
                    }
                    else
                    {

                        return true;
                    }
                case EUnitClass.Ranger:
                    if (otherUnit.UnitClass is EUnitClass.Mage && otherUnit.UnitClass is EUnitClass.Dragon)
                    {
                        return false;
                    }
                    else
                    {

                        return true;
                    }
                case EUnitClass.Mage:
                    if (otherUnit.UnitClass is EUnitClass.Mage)
                    {
                        return false;
                    }
                    else
                    {

                        return true;
                    }
                case EUnitClass.Imp:
                    if (otherUnit.UnitClass is EUnitClass.Dragon)
                    {
                        return false;
                    }
                    else
                    {

                        return true;
                    }
                case EUnitClass.Orc:
                    if (otherUnit.UnitClass is EUnitClass.Dragon)
                    {
                        return false;
                    }
                    else
                    {

                        return true;
                    }
                case EUnitClass.Dragon:
                    return true;

                default:
                    return false;
                
            }
            
            //return false;
        }

        public virtual bool Interact(Prop prop)
        {
            if (UnitClass is EUnitClass.Villager) return true;
            else return false;
        }



        
        public bool Move(Position targetPosition)
        {
            float canMove = (float)Math.Sqrt(Math.Pow(targetPosition.x, 2) + Math.Pow(targetPosition.y, 2));

            if (canMove <= MoveRange)
            {
                CurrentPosition = targetPosition;

                return true;
            }
            else return false;
        }
    }
}