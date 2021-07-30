export class Address {
    public city: string;
    public country: string;
    public street: string;
    public zipCode: string;
    public attention: string;
    public careOf: string;

    constructor(data: Partial<Address>) {
        Object.assign(this, data);
    }

    public static empty(): Address {
        return new Address({
            city: '',
            country: '',
            street: '',
            zipCode: '',
            attention: '',
            careOf: ''
        });
    }

}
