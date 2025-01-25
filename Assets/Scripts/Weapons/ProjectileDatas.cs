public struct ProjectileDatas
{
    public ProjectileDatas(int _damage, float _speed, float _duration, float _scaleModifier)
    {
        damage = _damage;
        speed = _speed;
        duration = _duration;
        areaSize = _scaleModifier;
    }

    public int damage; // + percentage
    public float speed; // + percentage
    public float duration; // + percentage
    public float areaSize; // + percentage


    //int armor // + flat
    //int health // + percentage
    //float regen // + flat
    //float movespeed // + percentage
    //int ability haste // + percentage
    //int projectile count // + flat

    //int exp

    //float pickup radius // + percentage
    // int critChance // + percentage

}
