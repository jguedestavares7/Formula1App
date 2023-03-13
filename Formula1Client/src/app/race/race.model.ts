import { Driver } from "../driver/driver.model";

export class Race {
    public id?: number;
    public name: string;
    public country: string;
    public numberLaps: number;
    public date: string;

    public winnerDriverId?: number;
    public winnerDriver?: Driver; 

    constructor(name: string, cntry: string, numLaps: number, dt: string, wnDrId?: number, wnDr?: Driver, id?: number){
        this.name = name;
        this.country = cntry;
        this.numberLaps = numLaps;
        this.date = dt;
        this.winnerDriverId = wnDrId;
        this.winnerDriver = wnDr;
        this.id = id;
    }
}