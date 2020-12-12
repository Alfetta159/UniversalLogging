import React, { useState } from 'react';
import SelectableDropDownItem from './SelectableDropDownItem';
import {
    UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem
} from 'reactstrap';
// import SelectableDropDownItemAllButton from './SelectableDropDownItemAllButton';

const SeverityNavDropDown = (props) => {
    const [allSelected, setAllSelected] = useState(false);

    // const items = await fetch('http://localhost:7071/api/severities')
    //     .then(response => response.json())
    //     .then(data => data);
    // console.log(items); // ?

    let index = 0;
    return (
        <UncontrolledDropdown nav inNavbar>
            <DropdownToggle nav caret>
                Severity
            </DropdownToggle>
            <DropdownMenu right>
                {/*<SelectableDropDownItemAllButton selected={allSelected} id='severity' DisplayName='All' Description='Select all options' />*/}
                <DropdownItem divider />
                {
                    fetch('http://localhost:7071/api/severities')
                        .then(response => response.json())
                        .then(data => data.map(d => <SelectableDropDownItem key={'SelectableDropDownItem' + index++} DisplayName={d.DisplayName} Description={d.Description} />))
                }
            </DropdownMenu>
        </UncontrolledDropdown>
    );
}

export default SeverityNavDropDown;