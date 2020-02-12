import { Component, OnInit } from "@angular/core";
import { MatchService } from "src/app/core/services/match.service";
import { CatMatch } from "src/app/core/models/cat-match.model";
import { CommonResponse } from "src/app/core/models/common.response.model";
import { Cat } from "src/app/core/models/cat.model";


@Component({
    selector: 'match-view',
    templateUrl: './match-view.component.html',
    styleUrls: ['./match-view.component.scss']
})
export class MatchViewComponent implements OnInit {

    public currentMatch: CatMatch = null;

    public constructor(private matchService: MatchService) { }

    public ngOnInit(): void {
        this.loadNextmatch();
    }

    public resolveMatch(winner: Cat): void {
        this.matchService.resolveMatch(this.currentMatch.left.id, this.currentMatch.right.id, winner.id).subscribe(_ => {
            this.loadNextmatch();
        });
    }

    public nextMatch(): void {
        this.loadNextmatch();
    }

    public loadNextmatch(): void {
        this.currentMatch = null;
        this.matchService.getMatch().subscribe((catMatch: CommonResponse<CatMatch>) => {
            if (catMatch.internalCode == 200) {
                this.currentMatch = catMatch.payload;
            }
        });
    }
}