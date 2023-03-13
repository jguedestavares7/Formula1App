import { Team } from "../team/team.model";
import { Race } from "../race/race.model";

export class Driver {
    public id?: number;
    public name: string;
    public number: number;
    public abbreviation: string;
    public nationality: string;
    public birthday: string;

    public teamId?: number;
    public team?: Team;

    public races?: Array<Race>;

    constructor(name: string, num: number, abbr: string, nat: string, birth: string, teamId?: number, team?: Team, racesList?: Array<Race>, id?: number){
        this.name = name;
        this.number = num;
        this.abbreviation = abbr;
        this.nationality = nat;
        this.birthday = birth;
        this.teamId = teamId;
        this.team = team;
        this.races = racesList;
        this.id = id;
    }
}