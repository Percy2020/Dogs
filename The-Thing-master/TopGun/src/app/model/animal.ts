export class Animal {

    public id: number;
    public dateOfBirth: Date;
    public type: string;
    public name: string;
    public breed: string;
    public gender: string;
    public size: string;
    public description: string;
    public photo: string;
    public ownerId: number;
    public ownerSocialSecurityNumber: string;

    constructor(data: Partial<Animal>) {
        Object.assign(this, data);
    }

    public static empty(): Animal {
        return new Animal({
            id: 0,
            dateOfBirth: new Date(),
            type: '',
            name: '',
            breed: '',
            gender: '',
            size: '',
            description: '',
            photo: '',
            ownerId: 0,
            ownerSocialSecurityNumber: '',
        });
    }

}
