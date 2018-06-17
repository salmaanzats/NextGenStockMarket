import { Component, OnInit, Input } from '@angular/core';
import { BlockUiService } from '../../core/services/block-ui.service';

@Component({
  selector: 'app-block-ui',
  templateUrl: './block-ui.component.html',
  styleUrls: ['./block-ui.component.css']
})
export class BlockUiComponent implements OnInit {

  @Input() isBlocked = false;
  @Input() loadMessage;
  constructor(blockUIService: BlockUiService) { 
    blockUIService.start$.subscribe(message => { this.isBlocked = message; });
  }

  ngOnInit() {
  }

}
