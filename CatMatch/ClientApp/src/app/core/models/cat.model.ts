import { MatchInfos } from "./match.infos.model";
import { Rank } from "./rank.model";

export interface Cat {
    id: number;
    imageLink: string;
    informations: MatchInfos;
    rank: Rank;
}