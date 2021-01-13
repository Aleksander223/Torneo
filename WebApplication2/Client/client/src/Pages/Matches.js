import React from 'react';
import Container from '@material-ui/core/Container';
import CssBaseline from '@material-ui/core/CssBaseline';
import EnhancedTable from '../Components/EnhancedMatchTable'
import namor from "namor";
import UserTableToolbar from "../Components/MatchTableToolbar";
import axios from 'axios';

const range = len => {
    const arr = []
    for (let i = 0; i < len; i++) {
      arr.push(i)
    }
    return arr
  }
  
  const newPerson = () => {
    const statusChance = Math.random()
    return {
      firstName: namor.generate({ words: 1, numbers: 0 }),
      lastName: namor.generate({ words: 1, numbers: 0 }),
      age: Math.floor(Math.random() * 30),
      visits: Math.floor(Math.random() * 100),
      progress: Math.floor(Math.random() * 100),
      status:
        statusChance > 0.66
          ? 'relationship'
          : statusChance > 0.33
          ? 'complicated'
          : 'single',
    }
  }
  
  function makeData(...lens) {
    const makeDataLevel = (depth = 0) => {
      const len = lens[depth]
      return range(len).map(d => {
        return {
          ...newPerson(),
          subRows: lens[depth + 1] ? makeDataLevel(depth + 1) : undefined,
        }
      })
    }
  
    return makeDataLevel()
  }

export default function Users() {
    const columns = React.useMemo(
        () => [
          {
            Header: "Id",
            accessor: 'id'
          },
          {
            Header: 'Game',
            accessor: 'game.name',
          },
          {
            Header: 'Winner',
            accessor: 'winner',
          },
          {
            Header: 'Team 1',
            accessor: 'teamMatches[0].team.name',
          },
          {
            Header: 'Team 2',
            accessor: 'teamMatches[1].team.name',
          },
          {
            Header: 'Score 1',
            accessor: 'score1',
          },
          {
            Header: 'Score 2',
            accessor: 'score2',
          },
          {
              Header: 'Tournament',
              accessor: 'tournament.name'
          }
        ],
        []
      )
    
      const [data, setData] = React.useState(React.useMemo(() => makeData(20), []))
      const [skipPageReset, setSkipPageReset] = React.useState(false)
      const [calledGet, setCalledGet] = React.useState(false);
    
      // We need to keep the table from resetting the pageIndex when we
      // Update data. So we can keep track of that flag with a ref.
    
      // When our cell renderer calls updateMyData, we'll use
      // the rowIndex, columnId and new value to update the
      // original data
      const updateMyData = (rowIndex, columnId, value) => {
        // We also turn on the flag to not reset the page
        const updateData = {};
        updateData[columnId] = value;

        if (columnId == "winner") {
          updateData.winnerId = parseInt(value);
        }

        console.log(updateData);

        axios.put("https://localhost:44356/matches/" + data[rowIndex].id, updateData, {
            withCredentials: true
        });

        setSkipPageReset(true)
        setData(old =>
          old.map((row, index) => {
            if (index === rowIndex) {
              return {
                ...old[rowIndex],
                [columnId]: value,
              }
            }
            return row
          })
        )
      }

      if (!calledGet) {
        setCalledGet(true);
        axios.get("https://localhost:44356/matches/", {withCredentials: true}).then((res) => {
        if (res.status === 200) {
          setData(res.data);
        } else {
        }
      }).catch((e) => {
      })
    }

      

    return <div>
    <CssBaseline />
    <Container>
    <EnhancedTable
      columns={columns}
      data={data}
      setData={setData}
      updateMyData={updateMyData}
      skipPageReset={skipPageReset}
      Toolbar={UserTableToolbar}
    />
    </Container>
  </div>
    
}