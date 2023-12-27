namespace ConsoleApp1
{
    enum Gender { Male, Female }
    class FamilyMember
    {
        public string name { get; set; }
        public Gender gender { get; set; }
        public FamilyMember[] children { get; set; }
        public FamilyMember mother { get; set; }
        public FamilyMember father { get; set; }

        public FamilyMember()
        {
        }

        public FamilyMember(string name, Gender gender, FamilyMember mother, FamilyMember father, params FamilyMember[]? familyMembers)
        {
            this.name = name;
            this.gender = gender;
            this.mother = mother;
            this.father = father;
            this.children = familyMembers;
        }

        public void MothersInFamily()
        {
            FamilyMember adult = this;

            if (adult.mother != null)
            {
                adult = adult.children.Length > 0 && adult.children[0].mother != null ? adult.children[0].mother : this;
            }

            while (adult.mother != null)
                adult = adult.mother;

            if (adult.gender == Gender.Female)
                Console.Write($"{adult.name} -> ");


            bool femaleChild = true;
            while (femaleChild)
            {
                femaleChild = false;
                Console.Write($"{adult.name} -> ");

                foreach (FamilyMember child in adult.children)
                    if (child.gender == Gender.Female)
                    {
                        adult = child;
                        femaleChild = true;
                        break;
                    }
            }
        }

        public void PrintFamily()
        {
            FamilyMember secondMember = null;
            if (this.children != null)
                secondMember = this.gender == Gender.Male ? this.children[0].mother : this.children[0].father;
            if (secondMember != null)
                PrintFamily(this, secondMember);
            else
                PrintFamily(this);

        }

        private void PrintFamily(params FamilyMember[] familyMembers)
        {
            List<FamilyMember> children = new List<FamilyMember>();
            foreach (FamilyMember familyMember in familyMembers)
                Console.Write($"{familyMember.name} ");
            Console.WriteLine();
            foreach (FamilyMember familyMember in familyMembers)
            {
                if (familyMember.children != null)
                {
                    foreach (FamilyMember child in familyMember.children)
                    {
                        if (child.children != null)
                        {
                            foreach (FamilyMember child2 in child.children)
                            {
                                FamilyMember second = child.gender == Gender.Male ? child2.mother : child2.father;
                                if (second != null && !children.Contains(second))
                                    children.Add(second);
                            }

                        }
                        if (!children.Contains(child))
                            children.Add(child);
                    }
                }
            }
            if (children.Count > 0)
                PrintFamily(children.ToArray());
        }

    }
}
