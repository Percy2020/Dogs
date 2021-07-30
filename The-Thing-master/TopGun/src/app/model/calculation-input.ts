export class CalculationInput {

    public income: string;
    public costs: string;
    public acquisitionCost: string;
    public dividend: string;


    public constructor(
        income: string,
        costs: string,
        acquisitionCost: string,
        dividend: string,
    ) {
        this.income = income;
        this.costs = costs;
        this.acquisitionCost = acquisitionCost;
        this.dividend = dividend;

    }

    public static empty(): CalculationInput {
        return new CalculationInput('', '', '', '');
    }

}

