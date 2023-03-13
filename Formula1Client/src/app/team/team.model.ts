import { Driver } from "../driver/driver.model";

export class Team {
    public id?: number;
    public name: string;
    public carName: string;
    public engine: string;
    public director: string;

    public driversIds?: [];

    constructor(name: string, carName: string, engine: string, director: string, listDrivers?: [], id?: number){
        this.name = name;
        this.carName = carName;
        this.engine = engine;
        this.director = director;
        this.driversIds = listDrivers;
        this.id = id;
    }
}